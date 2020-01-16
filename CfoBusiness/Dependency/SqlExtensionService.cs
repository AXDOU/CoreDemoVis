
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using Cfo.DTO.Base;
using CfoMiddleware.Extension;
using CfoMiddleware.Interface;

namespace CfoBusiness.Dependency
{
    [AutofacAutoRegisterAttribute]
    public  class SqlExtensionService: ISqlExtensionService
    {
        public void AddTableByEntity<T>()
        {
            T t = Activator.CreateInstance<T>();
            t.ToSqlTableStruct();
        }
    }
}