using CfoDAL.DataBase;
using Microsoft.Extensions.Configuration;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CfoDAL.DataBase

{
    public class SqlSugarFactory: ISqlSugarFactory
    {
        private SqlSugarClient _context;

        public SqlSugarFactory()
        {
            if (_context == null)
                _context = new SqlSugarClient(new ConnectionConfig
                {
                    DbType = DbType.SqlServer,
                    ConnectionString = DbConfig.ConnectionString,
                    InitKeyType = InitKeyType.Attribute,//从实体中读取自增列信息
                    IsAutoCloseConnection = true
                });
        }

        public SqlSugarClient InitSqlSugarClient { get { return this._context; } }

        private void Test()
        {


        }
    }
}