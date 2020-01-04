using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using CfoDAL.DataEntity;
using SqlSugar;

namespace CfoDAL.DataBase
{
    public class DbContext<T> where T : class, new()
    {
        public DbContext()
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
        public SimpleClient<CoreUser> Userdb { get { return new SimpleClient<CoreUser>(context); } }

        public SimpleClient<T> Currentdb => new SimpleClient<T>(context);
        public virtual List<T> GetList()
        {
            return Currentdb.GetList();
        }

        public virtual bool Delete(int id)
        {
            return Currentdb.DeleteById(id);
        }

        public virtual bool Update(T data)
        {
            return Currentdb.Update(data);
        }

        public virtual async Task<bool> Edit(T data)
        {
            return await Currentdb.AsUpdateable(data).ExecuteCommandAsync() > 0;
        }

        public virtual bool Insert(T data)
        {
            return Currentdb.Insert(data);
        }
    }
}
