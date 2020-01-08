using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using log4net;
using Microsoft.AspNetCore.Mvc.Filters;

namespace CoreDemoVis.Models
{
    /// <summary>
    /// 全局错误处理过滤
    /// </summary>
    public class HttpGlobalExceptionFilter : IExceptionFilter
    {
        private ILog _log = LogManager.GetLogger(AutofacConfigure.log4Repository.Name, typeof(HttpGlobalExceptionFilter));

        public void OnException(ExceptionContext context)
        {
            _log.Error(context.Exception);
        }

    }
}
