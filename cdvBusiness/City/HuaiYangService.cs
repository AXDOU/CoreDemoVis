
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CfoMiddleware.Interface;

namespace CfoBusiness.City
{
    public class LongDuService : ICity
    {
        public string City()
        {
            return "淮阳";
        }

        public string HistoryExtension()
        {
            return "泱泱华夏，伏羲女娲";
        }
    }
}