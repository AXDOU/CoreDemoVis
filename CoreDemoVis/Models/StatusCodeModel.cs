using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Threading.Tasks;
//using log4net.Core;
using Microsoft.AspNetCore.Diagnostics;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.Extensions.Logging;

namespace CoreDemoVis
{
    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.Any, NoStore = true)]
    public class StatusCodeModel : PageModel
    {
        private readonly ILogger _logger;
        public string RequestId { get; set; }

        public bool ShowRequestId => !string.IsNullOrEmpty(RequestId);

        public string ErrorStatusCode { get; set; }

        public string OriginalURL { get; set; }
        public bool ShowOriginalURL => !string.IsNullOrEmpty(OriginalURL);

        public StatusCodeModel(ILogger<StatusCodeModel> logger)
        {
            _logger = logger;
        }

        public void OnGet(string code)
        {
            RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier;
            ErrorStatusCode = code;

            var statusCodeReExecuteFeature = HttpContext.Features.Get<IStatusCodeReExecuteFeature>();
            if (statusCodeReExecuteFeature != null)
            {
                OriginalURL = statusCodeReExecuteFeature.OriginalPath + statusCodeReExecuteFeature.OriginalPath + statusCodeReExecuteFeature.OriginalQueryString;
            }
        }
    }
}
