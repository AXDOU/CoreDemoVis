using CfoDAL.DataBase;
using CfoDAL.DataEntity;
using CfoMiddleware;
using CfoMiddleware.Interface;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CfoBusiness
{
    /// <summary>
    /// Autofac
    /// </summary>
    public class AutofacService : DbContext<CoreUser>, IAutofacService
    {

        private SqlSugarClient dbContext;
        public AutofacService()
        {
            dbContext = new SqlSugarFactory().InitSqlSugarClient;
        }
       
        public string AutofacAttribute { get { return "属性啊"; } }


        public CoreUser GetUser(int id)
        {
            return dbContext.Queryable<CoreUser>().First(x => x.ID == id);
        }


        public string HelloWorld()
        {
            return "Hello world !";
        }

        public List<string> ListGenders()
        {
            // keeping this simple
            return new List<string>() { "1", "Male" };
        }

    }
}