using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Expressions;
using System.Text;
using System.Threading.Tasks;
using CfoDAL.DataEntity;
using SqlSugar;

namespace CfoDAL.DataBase
{
    public class DbContext<T> where T : class, new()
    {
        private ISqlSugarFactory factory = new SqlSugarFactory();

        public DbContext()
        {
            if (context == null)
                context = factory.InitSqlSugarClient;

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

        /// <summary>
        /// 根据实体生成数据表
        /// </summary>
        /// <typeparam name="T"></typeparam>
        public virtual void AddTableByEntity<T>()
        {
            context.CodeFirst.SetStringDefaultLength(200/*设置varchar默认长度为200*/).InitTables(typeof(T));//执行完数据库就有这个表了
        }

        public virtual T FindByClause(Expression<Func<T, bool>> predicate)
        {
            return context.Queryable<T>().Where(predicate).First();
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
