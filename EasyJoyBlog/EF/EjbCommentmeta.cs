using System;
using System.Collections.Generic;

namespace EasyJoyBlog.EF
{
    public partial class EjbCommentmeta
    {
        public ulong MetaId { get; set; }
        public ulong CommentId { get; set; }
        public string MetaKey { get; set; }
        public string MetaValue { get; set; }
    }
}
