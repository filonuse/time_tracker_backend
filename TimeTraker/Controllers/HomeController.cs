using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;

namespace TimeTraker.Controllers
{
    public class HomeController : Controller
    {
        public IActionResult Index()
        {
            var defaultPage = File("index.html", "text/html");
            return defaultPage;
        }
    }
}