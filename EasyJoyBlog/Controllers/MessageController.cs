using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using EasyJoyBlog.EF;
using Microsoft.AspNetCore.Mvc;
using EasyJoyBlog.Models;
using EasyJoyBlog.Blog;

namespace EasyJoyBlog.Controllers
{
    public class MessageController : Controller
    {
        public IActionResult Index()
        {
            ViewData["PageType"] = "Message";
            return View();
        }
        /// <summary>
        /// 留言
        /// </summary>
        /// <param name="author">作者</param>
        /// <param name="author_email">作者邮箱</param>
        /// <param name="content">回复内容</param>
        /// <param name="parent">父ID</param>
        /// <returns></returns>
        [HttpPost]
        public JsonResult CommentsPost(string author, string author_email, string content, ulong parent)
        {
            //检验传入数据
            using (var context = DataBaseContextConfig.CreateEasyJoyBlogContext())
            {
                EjbComments ejbComments = new EjbComments
                {
                    CommentPostId = 0,//0留言
                    CommentAuthor = author,
                    CommentAuthorUrl = "",
                    CommentAuthorIp = HttpContext.GetClientUserIp(),
                    CommentDate = DateTime.Now,
                    CommentDateGmt = DateTime.Now,
                    CommentContent = content,
                    CommentKarma = 0,
                    CommentApproved = "1",
                    CommentAgent = "",
                    CommentType = "",
                    UserId = 0,
                    CommentAuthorEmail = author_email,
                    CommentParent = parent
                };
               context.EjbComments.Add(ejbComments);  
                if (context.SaveChanges()>0)
                { 
                    return Json(new { type = "success", content = ejbComments.CommentId}); 
                }
                else
                    return Json(new { type = "error", content = "留言失败！" }); 
            }

        }

        /// <summary>
        /// 留言列表
        /// </summary> 
        /// <returns></returns>
        public JsonResult CommentsList()
        {
            List<Comments> ejbComments = new List<Comments>();
            using (var context = DataBaseContextConfig.CreateEasyJoyBlogContext())
            {
                //查询留言数据
                var Posts = context.EjbComments.Where(a => a.CommentPostId == 0&&a.CommentApproved=="1"&&a.CommentParent==0).OrderByDescending(a=>a.CommentDate);
                //查询回复的数据
                var CommentHfData= context.EjbComments.Where(a => a.CommentPostId == 0 && a.CommentApproved == "1" && a.CommentParent> 0);
                Comments comments;
                List<CommentHf> commentHfs;
                CommentHf commentHf;
                foreach (var item in Posts)
                {
                    commentHfs = new List<CommentHf>();
                    var HfData = CommentHfData.Where(a => a.CommentParent == item.CommentId);
                    if (HfData.Count()>0)//判断有回复
                    {
                        foreach (var j in HfData)
                        {
                            commentHf = new CommentHf()
                            {
                                atName =j.CommentAuthor,
                                hfContent=j.CommentContent,
                                hfName=item.CommentAuthor,
                                hfTime=j.CommentDate.ToString()
                             };
                            //添加回复条目
                            commentHfs.Add(commentHf);
                        } 
                    }
                    //给留言条目类赋值
                    comments = new Comments()
                    {
                        id = item.CommentId,
                        commentContent = item.CommentContent,
                        commentHfs = commentHfs,
                        commentName = item.CommentAuthor,
                        commentTime = item.CommentDate.ToString()
                    };
                    //添加留言条目
                    ejbComments.Add(comments);
                }
              
            }
            if (ejbComments.Count()>0)
            { 
                return Json(new { status = "ok", message="", datas = ejbComments });
            }
            else
            {
                return Json(new { status = "no", message = "", datas = ejbComments });
            }    

        }
    }
}