using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace EasyJoyBlog.Controllers
{
    public class ResumeController : Controller
    {
        public IActionResult Index()
        {
            ViewData["PageType"] = "Resume";
            return View();
        }
    }
}