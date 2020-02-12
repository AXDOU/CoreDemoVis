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
using System.Net;
using System.Text;
using System.Text.RegularExpressions;
using System.Drawing;
using System.Drawing.Printing;
using static System.Net.Mime.MediaTypeNames;
using static Cfo.Domain.HtmlToImg.IornToPdf;

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
            string filePath = @"G:\Test\";
            if (!System.IO.Directory.Exists(filePath))
                System.IO.Directory.CreateDirectory(filePath);
            string weburl = "https://www.atjubo.com/jbpay/Pay/HeTongAdvancePayment?OrderID=20201915768359886463&Type=1";
            string OrderNo = "20201915760255743183";
            string filepathname = filePath + OrderNo + ".png";// 

            //GeneratePdf();
            GenerateImage(weburl, filepathname);
            return View();
        }
     
        private void GeneratePdf()
        {
            //System.Diagnostics.ProcessStartInfo Info = new System.Diagnostics.ProcessStartInfo();
            //Info.FileName = @"H:\projects\CoreDemoVis\Cfo.Domain\libary\wkhtmltopdf.exe";
            //Info.WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden;
            //Info.CreateNoWindow = true;
            //string url = "localhost:44339/Home/Index";
            //Info.Arguments = $@"wkhtmltopdf --grayscale  www.baidu.com G:\\baidu.pdf";
            ////Info.Arguments = $@"-q --orientation Landscape {url} G:\\baidu.pdf";
            //Process proc = Process.Start(Info);
            //proc.WaitForExit();
            //proc.Close();

            ProcessStartInfo startInfo = new ProcessStartInfo();
            startInfo.FileName = @"H:\projects\CoreDemoVis\Cfo.Domain\libary\wkhtmltopdf.exe";
            startInfo.Arguments = "wkhtmltopdf --grayscale --disable-smart-shrinking --header-html head.html https://www.baidu.com G:\\baidu.pdf";
            startInfo.CreateNoWindow = true;
            startInfo.WindowStyle = ProcessWindowStyle.Hidden;
            var cc = Process.Start(startInfo);
            cc.WaitForExit();
            cc.Close();
        }


    
        //public static string GetHtml(string url, string charSet = "UTF-8")
        //{
        //    string strWebData = "";
        //    try
        //    {
        //        WebClient myWebClient = new WebClient();
        //        myWebClient.Credentials = CredentialCache.DefaultCredentials;
        //        byte[] myDataBuffer = myWebClient.DownloadData(url);
        //        strWebData = Encoding.Default.GetString(myDataBuffer);
        //        Match charSetMatch = Regex.Match(strWebData, "<meta([^<]*)charset=([^<]*)\"", RegexOptions.IgnoreCase | RegexOptions.Multiline);
        //        string webCharSet = charSetMatch.Groups[2].Value;
        //        if (charSet == null || charSet == "")
        //            charSet = webCharSet;
        //        if (charSet != null && charSet != "" && Encoding.GetEncoding(charSet) != Encoding.Default)
        //            strWebData = Encoding.GetEncoding(charSet).GetString(myDataBuffer);
        //    }
        //    catch { }
        //    return strWebData;
        //}


        private void GenerateImage(string weburl, string filepathname)
        {
            if (string.IsNullOrEmpty(weburl) || string.IsNullOrEmpty(filepathname))
                return;
            string str = @"H:\projects\CoreDemoVis\Cfo.Domain\libary\wkhtmltoimage.exe";//AppDomain.CurrentDomain.BaseDirectory + "bin\\wkhtmltopdf.exe";
            if (!System.IO.File.Exists(str))
                return;
            try
            {
                //$@"{AppDomain.CurrentDomain.BaseDirectory}\contractfile\Pay\{DateTime.Now.ToString("yyyy年MM月dd日")}\";
                //--image-dpi <integer>	当页面嵌入图像时，将它们缩小到指定的dpi尺寸(默认值600)
                //--image-quality <integer>  当使用jpeg算法压缩图像时，使用指定的图片质量(默认值94)
                ImageFormat imageFormat = ImageFormat.Jpg;
                ProcessStartInfo processInfo = new ProcessStartInfo
                {
                    FileName = @"H:\projects\CoreDemoVis\Cfo.Domain\libary\wkhtmltoimage.exe",
                    CreateNoWindow = true,
                    WindowStyle = System.Diagnostics.ProcessWindowStyle.Hidden,
                    Arguments = $"--quality {100} --width {500} -f {imageFormat} {weburl} {filepathname}"
                };
          
      
                Process p = Process.Start(processInfo);
                //new ProcessStartInfo(str, $"--quality {100} --width {500} -f {imageFormat} {weburl} {filepathname}")
                p.WaitForExit();
                p.Close();
                Stream stream = FileToStream(filepathname);
                if (System.IO.File.Exists(filepathname))
                {
                    System.IO.File.Delete(filepathname);
                }
            }
            catch (Exception ex)
            {
                return;
            }
        }


        //private void CompressedImage(string fileName, long quality)
        //{
        //    FileStream fs = new FileStream(fileName, FileMode.Open, FileAccess.Read);
        //    Byte[] bytePic = new Byte[fs.Length];
        //    fs.Read(bytePic, 0, bytePic.Length);
        //    MemoryStream stream = new MemoryStream(bytePic);
        //    Bitmap bmp = (Bitmap)Image.FromStream(stream);
        //    ImageCodecInfo myImageCodecInfo = ImageCodecInfo.GetImageEncoders()[1];  //如果下面遍历没有这种图片格式，就默认为jpeg
        //    ImageCodecInfo[] encoders = myImageCodecInfo.GetImageEncoders();
        //    for (int j = 0; j < encoders.Length; j++)
        //    {
        //        if (encoders[j].MimeType == "image/jpeg")
        //        {
        //            myImageCodecInfo = encoders[j];
        //            break;
        //        }
        //    }
        //    System.Drawing.SizeConverter myEncoder = System.Drawing.Imaging.Encoder.Quality;  //要操作的是质量
        //    EncoderParameters myEncoderParameters = new EncoderParameters(1);      //一个成员，只处理质量
        //    EncoderParameter myEncoderParameter = new EncoderParameter(myEncoder, quality);    //0为最差质量,100为最好，注意是long类型
        //    myEncoderParameters.Param[0] = myEncoderParameter;
        //    Size s = new Size(bmp.Width, bmp.Height);
        //    Bitmap newBmp = new Bitmap(bmp, s);
        //    MemoryStream ms = new MemoryStream();
        //    newBmp.Save(ms, myImageCodecInfo, myEncoderParameters);    //压缩后的流保存到ms
        //    //从流中还原图片
        //    Image image = Image.FromStream(ms);
        //    string curDirectory = Path.GetDirectoryName(Assembly.GetCallingAssembly().GetModules()[0].FullyQualifiedName) + "\\";
        //    //保存图片
        //    image.Save(curDirectory + "pic.jpg");
        //    fs.Dispose();
        //    stream.Dispose();
        //    newBmp.Dispose();
        //    ms.Dispose();

        //}



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
