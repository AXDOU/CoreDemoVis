using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Mvc;
using log4net;
using Microsoft.AspNetCore.Hosting;
using CoreDemoVis.Models;
using System.Diagnostics;

namespace CoreDemoVis.Controllers
{
    public class Log4Controller : Controller
    {
        private ILog log;

        public Log4Controller(IHostingEnvironment hostingEnv)
        {
            this.log = LogManager.GetLogger(AutofacConfigure.log4Repository.Name, typeof(Log4Controller));
        }

        public IActionResult Index()
        {
            log.Error("测试日志");
            log.Warn("警告日志");
            log.Debug("调试日志");
            return View();
        }

    }
}