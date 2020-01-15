using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using CfoMiddleware;
using CfoMiddleware.Interface;
using Microsoft.AspNetCore.Mvc;
using System.Text;
using System.Data;
using CfoBusiness.City;
using CfoBusiness.Dynasty;

namespace CoreDemoVis.Controllers
{

    /// <summary>
    /// 控制反转|依赖注入 |一个接口，多个实现的注入问题
    /// </summary>
    public class IOCController : Controller
    {
        private IAutofacService _autofacService;
        //第一种

        private ICity _nanYangService;
        private ICity _huaiYangService;

        private Func<string, IDynasty> _func;
        private IDynasty Qin;
        private IDynasty Tang;
        private IDynasty Ming;

      
        public IOCController(IAutofacService autofacService, IEnumerable<ICity> city, Func<string, IDynasty> dynasties)
        {
            this._autofacService = autofacService;
            this._huaiYangService = city.FirstOrDefault(x => x.GetType().Name.Contains("LongDu"));
            this._nanYangService = city.FirstOrDefault(x => x.GetType().Name.Contains("NanYang"));

            this._func = dynasties;
            this.Qin = _func("Qin");
            this.Ming = _func("Ming");
            this.Tang = _func("Tang");
        }

        public ActionResult Index()
        {
            
            string str = _nanYangService.GetHashCode() + " | " + HttpContext.RequestServices.GetService(typeof(ICity)).GetHashCode();
          
            //TestAutofac();
            ViewBag.NanYang = GetCity<NanYangService>(_nanYangService);
            ViewBag.LongDu = GetCity<LongDuService>(_huaiYangService);

            ViewBag.Qin = GetDynasty<Qin>(Qin);
            ViewBag.Tang = GetDynasty<Tang>(Tang);
            ViewBag.Ming = GetDynasty<Ming>(Ming);
            return View();
        }

        private string GetCity<T>(ICity city) where T : ICity
        {
            return city.City() + Environment.NewLine + city.HistoryExtension();
        }


        private string GetDynasty<T>(IDynasty dynasty) where T : IDynasty
        {
            return dynasty.Dynasty() + Environment.NewLine + dynasty.Emperor() + dynasty.GetGuid();
        }

        private void TestAutofac()
        {
            var attribute = _autofacService.AutofacAttribute;
            var user = _autofacService.GetUser(1);
            var str = _autofacService.HelloWorld();
        }
    }
}