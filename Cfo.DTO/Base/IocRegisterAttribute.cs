
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cfo.DTO.Base
{
    /// <summary>
    /// IOC注册依赖属性
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class AutofacAutoRegisterAttribute  : Attribute
    {

    }
}