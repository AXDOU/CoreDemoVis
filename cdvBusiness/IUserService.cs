using System;
using System.Collections.Generic;
using System.Text;
using CfoDAL.DataEntity;

namespace CfoMiddleware
{
    public interface IUserService
    {

        string GetAutofacString();

        List<CoreUser> GetUsers();

        string AutofacName { get; }
    }
}
