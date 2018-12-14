using System;
using System.Collections.Generic;

namespace EasyJoyBlog.EF
{
    public partial class EjbPostmeta
    {
        public ulong MetaId { get; set; }
        public ulong PostId { get; set; }
        public string MetaKey { get; set; }
        public string MetaValue { get; set; }
    }
}
