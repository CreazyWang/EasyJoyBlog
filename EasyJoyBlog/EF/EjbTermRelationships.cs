using System;
using System.Collections.Generic;

namespace EasyJoyBlog.EF
{
    public partial class EjbTermRelationships
    {
        public ulong ObjectId { get; set; }
        public ulong TermTaxonomyId { get; set; }
        public int TermOrder { get; set; }
    }
}
