using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyJoyBlog.Models
{
    public class BlogArticle
    {
        public BlogArticle() { }
        public BlogArticle(string img, string title, string blogInfo,Dictionary<string,string> className, string dTime, int viewNum, ulong link)
        {
            Img = img;
            Title = title;
            BlogInfo = blogInfo;
            ClassName = className;
            DTime = dTime;
            ViewNum = viewNum;
            Link = link;
        }

        public string Img { set; get; }
        public string Title { set; get; }
        public string BlogInfo { set; get; }
        public Dictionary<string, string> ClassName { set; get; }
        public string DTime { set; get; }
        public int ViewNum { set; get; }
        public ulong Link { set; get; }
    }
}
