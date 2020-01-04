using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using SqlSugar;

namespace CfoDAL.DataBase
{
    public class DbContextFactory
    {
        public DbContextFactory()
        {
            if (context == null)
                context = new SqlSugarClient(new ConnectionConfig
                {
                    DbType = DbType.SqlServer,
                    ConnectionString = "server=.;uid=sa;pwd=sa123;database=CoreDemo",
                    InitKeyType = InitKeyType.Attribute,//从实体中读取自增列信息
                    IsAutoCloseConnection = true
                });

            context.Aop.OnLogExecuting = (sql, pars) =>
            {
                Console.WriteLine(sql + "\r\n" +
                context.Utilities.SerializeObject(pars.ToDictionary(it => it.ParameterName, it => it.Value)));
                Console.WriteLine();
            };
        }

        public SqlSugarClient context;
        public SimpleClient Currentdb => new SimpleClient(context);
    }
}
