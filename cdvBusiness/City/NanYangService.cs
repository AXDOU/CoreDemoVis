
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using CfoMiddleware.Interface;

namespace CfoBusiness.City
{
    public class NanYangService:ICity
    {

        public string City()
        {
            return "南阳";
        }

        public string HistoryExtension()
        {
            return "南阳诸葛庐，西蜀子云亭";
        }
    }
}