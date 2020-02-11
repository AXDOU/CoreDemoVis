using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cfo.Domain.HtmlToImg;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemoVis.Areas.Test.Controllers
{
    public class DefaultController : Controller
    {
        public IActionResult Index()
        {
            IornToPdf ipornToPdf = new IornToPdf();
            ipornToPdf.ToPdf();
            return View();
        }
    }
}