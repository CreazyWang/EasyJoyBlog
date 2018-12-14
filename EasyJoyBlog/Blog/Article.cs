using System;
using System.Collections.Generic;
using System.Text; 

namespace EasyJoyBlog.Blog
{
    public class Article
    {
        /// <summary>
        /// 文章摘要获取
        /// </summary>
        /// <param name="content">文章内容</param>
        /// <param name="Size">保留文字个数</param>
        /// <returns></returns>
        public static string BlogInfo(string content,int Size) {
            //去除Html标记
            content = System.Text.RegularExpressions.Regex.Replace(content, "<[^>]+>", "");
            content = System.Text.RegularExpressions.Regex.Replace(content, "&[^;]+;", "");
            //保留文字
            if (content.Length > Size)
                content = content.Substring(0, Size);
            return content;
        }

       
    }
}
