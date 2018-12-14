using System;
using System.Collections.Generic;

namespace EasyJoyBlog.EF
{
    public partial class EjbTerms
    {
        public ulong TermId { get; set; }
        public string Name { get; set; }
        public string Slug { get; set; }
        public long TermGroup { get; set; }
    }
}
