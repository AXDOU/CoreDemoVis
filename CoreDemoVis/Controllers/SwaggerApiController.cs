using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;

namespace CoreDemoVis.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class SwaggerApiController : ControllerBase
    {
        public string Helloworld()       {
            return "Hello world";
        }
    }
}