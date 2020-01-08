using System;
using System.Collections.Generic;
using System.Text;
using System.Threading.Tasks;
using CfoDAL.DataEntity;

namespace CfoMiddleware
{
    public interface IUserService
    {

        //string GetAutofacString();

        List<CoreUser> GetUsers();

        CoreUser GetUser(int id);

        CoreUser GetEnableUser(string loginName);

        bool Update(CoreUser user);

        Task<bool> Edit(CoreUser user);


        bool Delete(int id);

        bool Insert(CoreUser user);

    }
}
