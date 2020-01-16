
using System;
using System.Collections.Generic;
using System.Configuration;
using System.IO;
using System.Linq;
using System.Text;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;

namespace CfoDAL.DataBase
{
    public class DbConfig
    {
        private static string connectionStr = "CoreDemoVisContext";
        /// <summary>
        /// 获取连接字符串
        /// </summary>

        public static string ConnectionString
        {
            get
            {
                var build = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile("appsettings.json").Build();
                return build.GetConnectionString(connectionStr);
            }
        }
    }
}