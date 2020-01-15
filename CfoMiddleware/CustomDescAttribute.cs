
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CfoMiddleware
{
    /// <summary>
    /// 自定义·特性类
    /// </summary>
    [AttributeUsage(AttributeTargets.Property)]
    public class CustomDescAttribute:Attribute
    {

        public string Descroption { get; set; }
    }
}