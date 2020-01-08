
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CfoDAL.DataEntity;

namespace CfoMiddleware.Interface
{
    public interface IAutofacService
    {
        string AutofacAttribute { get; }

        string HelloWorld();

        CoreUser GetUser(int id);
    }
}