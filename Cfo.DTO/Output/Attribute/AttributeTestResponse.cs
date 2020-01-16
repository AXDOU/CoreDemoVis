using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Cfo.DTO.Base;

namespace Cfo.DTO.Output
{
    /// <summary>
    /// 特性测试输出类
    /// </summary>
    public class AttributeTestResponse 
    {

        /// <summary>
        /// id
        /// </summary>
        [CustomDesc(Descroption = "Id")]
        public int Id { get; set; }


        [CustomDesc(Descroption = "登录名")]
        /// <summary>
        /// 登录名
        /// </summary>
        public string LoginName { get; set; }

        public int Type { get; set; }
    }
}
