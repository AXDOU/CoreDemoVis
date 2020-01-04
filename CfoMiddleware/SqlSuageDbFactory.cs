using CfoDAL.DataBase;
using SqlSugar;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace CfoMiddleware
{
    public class SqlSugarFactory 
    {
        private SqlSugarClient _context;
        public SqlSugarClient Context { get { return this._context; } set{ this._context = value; } }
        public SqlSugarFactory()
        {
            if (_context == null)
                _context = new DbContextFactory().context;
        }

    }
}