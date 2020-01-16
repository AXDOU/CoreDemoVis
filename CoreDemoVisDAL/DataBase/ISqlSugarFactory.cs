using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;

namespace CfoDAL.DataBase
{
    public interface ISqlSugarFactory
    {
        SqlSugarClient InitSqlSugarClient { get; }
    }
}
