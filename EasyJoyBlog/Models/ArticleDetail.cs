using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyJoyBlog.Models
{
    public class ArticleDetail
    {
        public ArticleDetail() { }
        public ArticleDetail(ulong id, string term, string title, string author, string date, ulong click, Dictionary<string,string> tag, string content, string next, string last)
        {
            Id = id;
            Term = term;
            Title = title;
            Author = author;
            Date = date;
            Click = click;
            Tag = tag;
            Content = content;
            Next = next;
            Last = last;
        }

        public ulong Id { set; get; }
        public string Term { set; get; }
        public string Title { set; get; }
        public string Author { set; get; }
        public string Date { set; get; }
        public ulong Click { set; get; }
        public Dictionary<string, string> Tag { set; get; }
        public string Content { set; get; }
        public string Next{ set; get; }
        public string Last { set; get; }
      
    }
}
