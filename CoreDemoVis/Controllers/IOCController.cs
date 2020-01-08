using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CfoMiddleware.Interface;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemoVis.Controllers
{

    /// <summary>
    /// 控制反转|依赖注入
    /// </summary>
    public class IOCController : Controller
    {
        private IAutofacService _autofacService;
        public IOCController(IAutofacService autofacService)
        {
            this._autofacService = autofacService;
        }

        public ActionResult Index()
        {
            //TestAutofac();
            return View();
        }


        private void TestAutofac()
        {
            var attribute = _autofacService.AutofacAttribute;
            var user = _autofacService.GetUser(1);
            var str = _autofacService.HelloWorld();
        }
    }
}