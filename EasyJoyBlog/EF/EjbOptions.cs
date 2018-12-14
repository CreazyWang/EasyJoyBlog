using System;
using System.Collections.Generic;

namespace EasyJoyBlog.EF
{
    public partial class EjbOptions
    {
        public ulong OptionId { get; set; }
        public string OptionName { get; set; }
        public string OptionValue { get; set; }
        public string Autoload { get; set; }
    }
}
