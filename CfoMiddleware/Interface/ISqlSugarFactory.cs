using System;
using System.Collections.Generic;
using System.Text;
using SqlSugar;

namespace CfoMiddleware.Interface
{
    public interface ISqlSugarFactory
    {
        SqlSugarClient InitSqlSugarClient { get; }
    }
}
