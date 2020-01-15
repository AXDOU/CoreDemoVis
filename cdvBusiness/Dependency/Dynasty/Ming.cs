
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CfoMiddleware.Interface;

namespace CfoBusiness.Dynasty
{
    public class Ming : IDynasty
    {
        public string Dynasty()
        {
            return "宋代";
        }

        public string Emperor()
        {
            return "太祖 朱元璋";
        }

        public Guid GetGuid()
        {
            return Guid.NewGuid();
        }
    }
}