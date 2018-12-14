using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyJoyBlog.Models
{
    /// <summary>
    /// 回复条目
    /// </summary>
    public class Comments
    {
        public Comments() { }
        public Comments(string commentName, string commentTime, string commentContent, ulong id, List<CommentHf> commentHfs)
        {
            this.commentName = commentName;
            this.commentTime = commentTime;
            this.commentContent = commentContent;
            this.id = id;
            this.commentHfs = commentHfs;
        }

        public string commentName { set; get; }
        public string commentTime { set; get; }
        public string commentContent { set; get; }
        public ulong id { set; get; }
        public List<CommentHf> commentHfs { set; get; }
 
    }
    /// <summary>
    /// 回复子条目
    /// </summary>
    public class CommentHf
    {
        public CommentHf() { }
        public CommentHf(string hfName, string hfTime, string hfContent, string atName)
        {
            this.hfName = hfName;
            this.hfTime = hfTime;
            this.hfContent = hfContent;
            this.atName = atName;
        }

        public string hfName { set; get; }
        public string hfTime { set; get; }
        public string hfContent { set; get; }
        public string atName { set; get; }

    }
}
