
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using CfoDAL.DataBase;
using CfoDAL.DataEntity;
using CfoMiddleware;
using SqlSugar;

namespace CfoMiddleware
{
    public class UserManage : DbContext<CoreUser>
    {
        private SqlSugarClient dbContext;
        public UserManage()
        {
            dbContext = new SqlSugarFactory().Context;
        }

        //public bool Edit(CoreUser userModel)
        //{
        //    var coreUserEntity = dbContext.Queryable<CoreUser>().InSingle(userModel.ID);
        //    PropertyInfo[] pInfo = typeof(CoreUser).GetProperties();
        //    foreach (var item in pInfo)
        //    {
        //        if (item.Name.Equals("CreateTime"))
        //            continue;
        //        item.SetValue(coreUserEntity, item.GetValue(userModel, null), null);
        //    }
        //    return dbContext.Updateable(coreUserEntity).ExecuteCommand() > 0;
        //}

        //public bool Add(CoreUser userModel)
        //{
        //    return dbContext.Insertable(userModel).ExecuteCommand() > 0;
        //}


        //public bool Remove(int userId)
        //{
        //    var userEntity = dbContext.Queryable<CoreUser>().InSingle(userId);
        //    if (userEntity != null)
        //        return dbContext.Deleteable(userEntity).ExecuteCommand() > 0;
        //    return false;
        //}


        //public List<CoreUser> GetUsers()
        //{
        //    return dbContext.Queryable<CoreUser>().ToList();
        //}


        //public string GetAutofacString()
        //{
        //    return "hello world2";
        //}

        //public string AutofacName { get { return "你zui了2"; } }
    }
}