using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyJoyBlog.EF;
using EasyJoyBlog.Models;
using EasyJoyBlog.Blog;

namespace EasyJoyBlog.Controllers
{
    public class ArticleListController : Controller
    {
        public IActionResult Index(string type="")
        {
            ViewData["PageType"] = "ArticleList"; 
            using (var context = DataBaseContextConfig.CreateEasyJoyBlogContext())
            {
                //加载分类目录 
                List<EjbTerms> lvterms = context.EjbTerms.ToList();
                List<EjbTermRelationships> ejbTermRelationships = context.EjbTermRelationships.ToList();
                //分组查询分类（去掉无文章的分类）
                var terms = from a in lvterms
                            join b in ejbTermRelationships
                            on a.TermId equals b.TermTaxonomyId
                            group a by a.Name into a
                            select a;
                lvterms = new List<EjbTerms>();
                foreach (var item in terms)
                {
                    lvterms.Add(item.First());
                }
                //判断加载博文类型
                if (type == ""||type=="All")
                {
                    //加载全部文章
                    ViewData["blogtype"] = "All";
                }
                else
                {
                    ViewData["blogtype"] = type;
                }
                
                ViewData["Terms"] = lvterms; 
            }
            return View();
        }

        /// <summary>
        /// 博文详情
        /// </summary>
        /// <param name="id"></param>
        /// <returns></returns>
        public IActionResult Detail(ulong id = 0)
        {
            ViewData["PageType"] = "Detail";
            if (id == 0)
            {
                return RedirectToAction("Index", "Home", new { });
            }
            else
            {
                using (var context = DataBaseContextConfig.CreateEasyJoyBlogContext())
                {
                    //加载分类目录 
                    List<EjbTerms> lvterms = context.EjbTerms.ToList();
                    List<EjbTermRelationships> ejbTermRelationships = context.EjbTermRelationships.ToList();
                    var terms = from a in lvterms
                                join b in ejbTermRelationships
                                on a.TermId equals b.TermTaxonomyId
                                group a by a.Name into a
                                select a;
                    lvterms = new List<EjbTerms>();
                    foreach (var item in terms)
                    {
                        lvterms.Add(item.First());
                    } 

                    ViewData["Terms"] = lvterms;
                    EjbPosts ejbPosts = context.EjbPosts.Where(a => a.Id == id && a.PingStatus == "open" && a.PostStatus == "publish").FirstOrDefault();
                    if (ejbPosts == null)
                    {
                        return RedirectToAction("Index", "Home", new { });
                    }
                    else
                    {
                        //查询作者
                        EjbUsers ejbUsers = context.EjbUsers.Where(a => a.Id == ejbPosts.PostAuthor).FirstOrDefault();
                        Dictionary<string, string> TermName = new Dictionary<string, string>();
                        //加载标签关系 
                        var Termdata = from a in context.EjbTermRelationships.ToList()
                                       join b in context.EjbTerms.ToList()
                                       on a.TermTaxonomyId equals b.TermId
                                       where a.ObjectId == ejbPosts.Id
                                       select b;
                        foreach (var j in Termdata)
                        {
                            TermName.Add(j.Name, j.Slug);
                        }
                        ArticleDetail articleDetail = new Models.ArticleDetail()
                        {
                            Id = ejbPosts.Id,
                            Title = ejbPosts.PostTitle,
                            Content = ejbPosts.PostContent,
                            Date = ejbPosts.PostDate.ToShortDateString(),
                            Author = ejbUsers.DisplayName,
                            Click = 0,
                            Last = "文章1",
                            Next = "文章2",
                            Tag = TermName,
                            Term = ""
                        };
                        ViewData["ArticleDetail"] = articleDetail;
                    }
                } 
            }
            return View();
        }
   
        /// <summary>
        /// 加载博文列表
        /// </summary>
        /// <param name="type">博文slug，默认为空，加载全部博文</param>
        /// <returns></returns>
        public JsonResult BlogsList(string type="")
        {
            using (var context = DataBaseContextConfig.CreateEasyJoyBlogContext())
            {
            
                //查询文章
                var Posts = context.EjbPosts.Where(a => a.PingStatus == "open" && a.PostStatus == "publish").OrderByDescending(a => a.PostDate);
                //查询分类
                List<EjbTerms> Terms = context.EjbTerms.ToList();
                //查询文章分类关系
                List<EjbTermRelationships> TermRelationships = context.EjbTermRelationships.ToList();
                //博文列表视图模型列表
                List<BlogArticle> ejbPosts = new List<BlogArticle>();
                Dictionary<string, string> TermName; 
                if (type == ""||type=="All")
                {
                    //查询全部博文
                    foreach (var item in Posts)
                    {
                        TermName = new Dictionary<string, string>();
                        var Termdata = from a in TermRelationships
                                       join b in Terms
                                       on a.TermTaxonomyId equals b.TermId
                                       where a.ObjectId == item.Id
                                       select b;
                        foreach (var j in Termdata)
                        {
                            TermName.Add(j.Name, j.Slug);
                        }
                        //赋值给视图模型类
                        BlogArticle blogArticle = new BlogArticle()
                        {
                            BlogInfo = Article.BlogInfo(item.PostContent, 200),
                            ClassName = TermName,
                            DTime = item.PostDate.ToShortDateString(),
                            Link = item.Id,
                            Img = "images/text02.jpg",
                            Title = item.PostTitle,
                            ViewNum = 0
                        };
                        ejbPosts.Add(blogArticle); 
                    }
                }
                else
                {
                    //根据Type查询博文
                     var Blogs = from a in TermRelationships 
                                   from b in Terms 
                                   join c in Posts
                                   on a.ObjectId equals c.Id
                                   where b.Slug == type && a.TermTaxonomyId==b.TermId
                                   select c;
                    foreach (var item in Blogs)
                    {
                        var Termdata = from a in TermRelationships
                                       join b in Terms
                                       on a.TermTaxonomyId equals b.TermId
                                       where a.ObjectId == item.Id
                                       select b;
                        TermName = new Dictionary<string, string>();
                        foreach (var j in Termdata)
                        {
                            TermName.Add(j.Name, j.Slug);
                        }
                        //赋值给视图模型类
                        BlogArticle blogArticle = new BlogArticle()
                        {
                            BlogInfo = Article.BlogInfo(item.PostContent, 200),
                            ClassName = TermName,
                            DTime = item.PostDate.ToShortDateString(),
                            Link = item.Id,
                            Img = "images/text02.jpg",
                            Title = item.PostTitle,
                            ViewNum = 0
                        };
                        ejbPosts.Add(blogArticle); 
                    }
                }
                if (ejbPosts.Count() > 0)
                {
                    return Json(new { status = "ok", message = "", datas = ejbPosts });
                }
                else
                {
                    return Json(new { status = "no", message = "", datas = ejbPosts });
                }
            }

        }
    }
}