using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using EasyJoyBlog.Models;
using System.Text;
using EasyJoyBlog.EF;
using EasyJoyBlog.Blog;

namespace EasyJoyBlog.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
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
                Dictionary<string, string> TermName = new Dictionary<string, string>();
                foreach (var item in Posts)
                {
                   //加载标签关系
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
                return View(ejbPosts);
            } 
        } 
    }
}
