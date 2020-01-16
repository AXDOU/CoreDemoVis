using System;
using System.Collections.Generic;
using System.Text;
using CfoMiddleware.Interface;

namespace CfoBusiness.Dynasty
{
    public class Qin : IDynasty
    {
        public Guid GetGuid()
        {
            return Guid.NewGuid();
        }

        public string Dynasty()
        {
            return "秦";
        }

        public string Emperor()
        {
            return "始皇 嬴政"; 
        }
    }
}
