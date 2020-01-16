using System;
using System.Collections.Generic;
using System.Text;

namespace CfoMiddleware.Interface
{
    public interface ISqlExtensionService
    {
        //根据实体类创建表
        void AddTableByEntity<T>();
    }
}
