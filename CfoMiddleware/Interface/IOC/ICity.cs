using System;
using System.Collections.Generic;
using System.Text;

namespace CfoMiddleware.Interface
{
    public interface ICity
    {
        string City();

        /// <summary>
        /// 历史延申
        /// </summary>
        /// <returns></returns>
        string HistoryExtension();
    }
}
