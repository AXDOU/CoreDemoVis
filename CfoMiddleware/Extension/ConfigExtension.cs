using System;
using System.Collections.Generic;
using System.Text;
using System.Configuration.Provider;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.Configuration.Json;
using System.IO;

namespace CfoMiddleware.Extension
{
    public static class ConfigExtension
    {
       /// <summary>
       /// 根据Key获取json配置信息
       /// </summary>
       /// <param name="key"></param>
       /// <returns></returns>
        public static string GetSection(string key)
        {
            var configuration = new ConfigurationBuilder().SetBasePath(Directory.GetCurrentDirectory()).AddJsonFile($"appsettings.json").Build();
            return configuration.GetSection(key).Value;
        }

    }
}
