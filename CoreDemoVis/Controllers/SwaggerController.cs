using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using Swashbuckle.AspNetCore;
namespace CoreDemoVis.Controllers
{
    public class SwaggerController : Controller
    {
        public IActionResult Index()
        {
            return View();
        }
    }
}