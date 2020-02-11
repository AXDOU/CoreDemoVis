using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using CoreDemoVis.Models;
using CfoDAL.DataBase;
using CfoDAL.DataEntity;
using CfoBusiness;
using Microsoft.AspNetCore.Hosting;
using log4net;
using Microsoft.AspNetCore.Authorization;
using System.Security.Claims;
using Microsoft.AspNetCore.Authentication;
using CfoMiddleware;
using Cfo.Domain.HtmlToImg;
using System.IO;

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
            //IornToPdf ipornToPdf = new IornToPdf();
            //ipornToPdf.ToPdf();

            string weburl = "http://www.atjubo.com/jbpay/Pay/HeTongYP?OrderID=20201915760255743183", filepathname = "G:\\baidu.png";
            //GeneratePdf();
            GenerateImage(weburl, filepathname);
            return View();
        }

        private void GeneratePdf()
        {
            System.Diagnostics.ProcessStartInfo Info = new System.Diagnostics.ProcessStartInfo();
            Info.FileName = @"H:\projects\CoreDemoVis\Cfo.Domain\libary\wkhtmltopdf.exe";
            Info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            Info.CreateNoWindow = true;
            string url = "localhost:44339/Home/Index";
            Info.Arguments = $@"-q --orientation Landscape {url} G:\\baidu.pdf";
            Process proc = Process.Start(Info);
            proc.WaitForExit();
            proc.Close();
        } 


        private void GenerateImage(string weburl,string filepathname)
        {
            try
            {

               
                string filePath = @"G:\Test\"; //$@"{AppDomain.CurrentDomain.BaseDirectory}\contractfile\Pay\{DateTime.Now.ToString("yyyy年MM月dd日")}\";
                if (!System.IO.Directory.Exists(filePath))
                    System.IO.Directory.CreateDirectory(filePath);
                string OrderNo = "20201915760255743183";
                weburl = "http://www.atjubo.com";//jbpay
                filepathname = filePath + OrderNo + ".png";// 
                if (string.IsNullOrEmpty(weburl) || string.IsNullOrEmpty(filepathname))
                    return;
                //H:\projects\CoreDemoVis\Cfo.Domain
                string str = @"H:\projects\CoreDemoVis\Cfo.Domain\libary\wkhtmltoimage.exe";// AppDomain.CurrentDomain.BaseDirectory + "bin\\wkhtmltopdf.exe";
                if (!System.IO.File.Exists(str))
                    return;
                using (Process p = System.Diagnostics.Process.Start(str, weburl + " " + filepathname))
                {
                    p.WaitForExit();
                    p.Close();
                }

                Stream stream = FileToStream(filepathname);
            }
            catch (Exception ex)
            {
               return ;
            }
        }

        public Stream FileToStream(string fileName)
        {
            // 打开文件
            FileStream fileStream = new FileStream(fileName, FileMode.Open, FileAccess.Read, FileShare.Read);
            byte[] bytes = new byte[fileStream.Length]; // 读取文件的 byte[]
            fileStream.Read(bytes, 0, bytes.Length);
            fileStream.Close();
            // 把 byte[] 转换成 Stream
            Stream stream = new MemoryStream(bytes);
            return stream;
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
    }
}
