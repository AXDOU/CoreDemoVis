using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreDemoVis.Models;
using CfoDAL.DataBase;
using CfoDAL.DataEntity;
using CfoMiddleware;


namespace CoreDemoVis.Controllers
{

    public class HomeController : Controller
    {

        private IUserService _service { get; set; }

        public HomeController(IUserService service)
        {
           this._service = service;
        }

        public void TestAutofac()
        {
            //var msg = _service.GetAutofacString();
            var users = _service.GetUsers();
            var str = _service.AutofacName;
        }

        public IActionResult Index()
        {
            TestAutofac();
            //CoreUser coreUser = new CoreUser
            //{
            //    Password = "122",
            //    Address = "地址",
            //    CoKey = "11",
            //    Email = "11",
            //    FullName = "tang1",
            //    Phone = "12346546551",
            //    Sex = 0
            //};
            //userManage.Insert(coreUser);

            //user2.CoKey = "11111";
            //user2.Email = "22@qq.com";
            //userManage.Update(user2);
            //userManage.Delete(2);

            //CoreUser coreUser2 = new CoreUser
            //{
            //    Password = "122",
            //    Address = "地址",
            //    CoKey = "11",
            //    Email = "11",
            //    FullName = "tang2",
            //    Phone = "12346546551",
            //    Sex = 0
            //};
            //userManage.Add(coreUser2);
            //var user3 = userService.GetUser(3);
            //user3.FullName = "tang22";
            //userManage.Edit(user3);
            //userManage.Remove(3);
            return View();
        }


        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        public IActionResult Contact()
        {
            ViewData["Message"] = "Your contact page.";

            return View();
        }

        public IActionResult Privacy()
        {
            return View();
        }

        [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
        public IActionResult Error()
        {
            return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
        }
    }
}
