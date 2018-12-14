using System;
using System.Collections.Generic;

namespace EasyJoyBlog.EF
{
    public partial class EjbUsermeta
    {
        public ulong UmetaId { get; set; }
        public ulong UserId { get; set; }
        public string MetaKey { get; set; }
        public string MetaValue { get; set; }
    }
}
