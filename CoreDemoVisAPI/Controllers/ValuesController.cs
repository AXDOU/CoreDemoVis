using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cfo.DTO.Base;
using Cfo.DTO.Output;
using CfoDAL.DataEntity;
using CfoBusiness;
using Microsoft.AspNetCore.Mvc;
using CfoMiddleware.Extension;
using CfoMiddleware;

namespace CoreDemoVisAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ValuesController : ControllerBase
    {
        private IUserService _userService { get; set; }

        //public ValuesController(IUserService userService)
        //{
        //    this._userService = userService;
        //}

        #region[ 原生]
        /// <summary>
        ///  this is test xml desctription
        /// </summary>
        /// <returns></returns>
        // GET api/values
        [HttpGet]
        public ActionResult<IEnumerable<string>> Get()
        {
            return new string[] { "value1", "value2" };
        }

        // GET api/values/5
        [HttpGet("{id}")]
        public ActionResult<string> Get(int id)
        {
            return "value";
        }

        // POST api/values
        [HttpPost]
        public void Post([FromBody] string value)
        {
        }

        // PUT api/values/5
        [HttpPut("{id}")]
        public void Put(int id, [FromBody] string value)
        {
        }

        // DELETE api/values/5
        [HttpDelete("{id}")]
        public void Delete(int id)
        {
        }
        #endregion


        #region[demo]
        /// <summary>
        /// 根据key值,获取配置内容
        /// </summary>
        /// <returns></returns>
        // GET api/values/6
        [HttpGet("key/{key}")]
        public ActionResult<string> GetSection(string key)
        {
            var res = ConfigExtension.GetSection(key);
            return res;
        }

        /// <summary>
        /// 获取特性描述性信息
        /// </summary>
        /// <returns></returns>
        [HttpGet("Attribute")]
        public ActionResult<ReturnResult> GetCustomeAttribute()
        {
            AttributeTestResponse attributeTest = new AttributeTestResponse();

            ReturnResult result = new ReturnResult
            {
                StatusCode = 1,
                Data = new { id = attributeTest.GetCustomDesc("Id"), LoginName = attributeTest.GetCustomDesc("LoginName") }
            };
            return result;
        }

        /// <summary>
        /// 根据实体生成Sql建表语句，含注释
        /// </summary>
        /// <returns></returns>
        [HttpGet("Entity")]
        public ActionResult<string> GetCreateSqlByEntity()
        {
            AttributeTestResponse attributeTest = new AttributeTestResponse();
            string res = attributeTest.DefaultStrSize(100).ToSqlTableStruct();
            return res;
        }

        /// <summary>
        /// SqlSugar获取实体
        /// </summary>
        /// <returns></returns>
        [HttpGet("Sugar")]
        public ActionResult<ReturnResult> GetSqlSugarResult()
        {
            CoreUser coreUser = _userService.FindByClause(x => x.ID < 100);
            ReturnResult result = new ReturnResult
            {
                Data = coreUser
            };
            return result;
        }

        #endregion
    }
}
