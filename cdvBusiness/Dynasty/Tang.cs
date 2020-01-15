
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CfoMiddleware.Interface;

namespace CfoBusiness.Dynasty
{
    public class Tang : IDynasty
    {
        public Guid GetGuid()
        {
            return Guid.NewGuid();
        }

        public string Dynasty()
        {
            return "唐朝";
        }

        public string Emperor()
        {
            return "太宗  李世民";
        }
    }
}