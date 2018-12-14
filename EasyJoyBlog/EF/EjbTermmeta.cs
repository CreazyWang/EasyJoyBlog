using System;
using System.Collections.Generic;

namespace EasyJoyBlog.EF
{
    public partial class EjbTermmeta
    {
        public ulong MetaId { get; set; }
        public ulong TermId { get; set; }
        public string MetaKey { get; set; }
        public string MetaValue { get; set; }
    }
}
