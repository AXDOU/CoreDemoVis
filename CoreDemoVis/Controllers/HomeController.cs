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
using Microsoft.AspNetCore.Hosting;
using log4net;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using CfoMiddleware.Extension;

namespace CoreDemoVis.Controllers
{

    public class HomeController : Controller
    {

        private IUserService _service { get; set; }
        private ILog log;

        public HomeController(IUserService service, IHostingEnvironment hostingEnv)
        {
            this._service = service;
            this.log = LogManager.GetLogger(AutofacConfigure.log4Repository.Name, typeof(Log4Controller));
        }

        [AllowAnonymous]
        public IActionResult Index()
        {
           
            //string desc = user.GetCustomDesc("Id");
            //string desc2 = user.GetCustomDesc("LoginName");

            CoreUser coreUser = _service.GetUser(1);
            string name = coreUser.FullName;
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


        [Authorize(Roles = "system")]
        public IActionResult About()
        {
            ViewData["Message"] = "Your application description page.";

            return View();
        }

        [Authorize(Roles = "admin")]
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


        /// <summary>
        /// 登录权限认证
        /// </summary>
        /// <returns></returns>
        [AllowAnonymous]
        public IActionResult Login()
        {
            return View();
        }


        [AllowAnonymous]
        [HttpPost]
        public async Task<IActionResult> Login(string loginName, string password, string returnUrl = null)
        {
            var user = _service.GetEnableUser(loginName);
            if (user != null || !user.Password.Equals(password))
            {
                string role = loginName == "15236551222" ? "admin" : "system";
                //身份证
                var claim = new List<Claim> { new Claim(ClaimTypes.Name, loginName), new Claim(ClaimTypes.Role, role) };
                //人
                var claimsIdentity = new ClaimsIdentity(claim, "Cookies");

                //凭证
                var claimsPrincipal = new ClaimsPrincipal(claimsIdentity);

                await HttpContext.SignInAsync(claimsPrincipal);
                return RedirectToAction("Index");
            }
            return RedirectToAction("Login");
        }

        [HttpGet]
        [AllowAnonymous]
        public async Task<IActionResult> LoginOut(string loginName)
        {
            bool isauthenticated = User.Identity.IsAuthenticated;
            if (isauthenticated)
            {
                await HttpContext.SignOutAsync("Cookies");
            }
            return RedirectToAction("Index");
        }


        /// <summary>
        /// 测试根据实体生成sql语句
        /// </summary>
        private void TestGenerateTableMethod()
        {
            Models.tb_user user = new tb_user();
            ///测试生成表结构信息
            string result = user.DefaultStrSize(100).ToSqlTableStruct();


            Post post = new Post();
            string res2 = post.ToSqlTableStruct();
        }
    }
}
