using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace EasyJoyBlog.EF
{
    public class DataBaseContextConfig
    {
        private const string EasyJoyBlogConnectionString = "server=139.199.106.202;userid=wp;pwd=a1160651865++;port=3306;database=EasyJoyBlog;";
        private const string LogConnectionString = "server=139.199.106.202;userid=wp;pwd=a1160651865++;port=3306;database=log;";
        /// <summary>
        /// 创建EasyJoyBlog数据库上下文
        /// </summary>
        /// <returns></returns>
        public static easyjoyblogContext CreateEasyJoyBlogContext()
        {
            var optionBuilder = new DbContextOptionsBuilder<easyjoyblogContext>();
            optionBuilder.UseMySql(EasyJoyBlogConnectionString);
            var context = new easyjoyblogContext(optionBuilder.Options);
            return context;
        }
        /// <summary>
        /// 创建log数据库上下文
        /// </summary>
        /// <returns></returns>
        //public static logContext CreateLogContext()
        //{
        //    var optionBuilder = new DbContextOptionsBuilder<logContext>();
        //    optionBuilder.UseMySql(LogConnectionString);
        //    var context = new logContext(optionBuilder.Options);
        //    return context;
        //}

    }
}
