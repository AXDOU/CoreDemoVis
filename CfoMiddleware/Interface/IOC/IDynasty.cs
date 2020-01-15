using System;
using System.Collections.Generic;
using System.Text;

namespace CfoMiddleware.Interface
{
    /// <summary>
    /// 朝代
    /// </summary>
    public interface IDynasty
    {
        Guid GetGuid();
       
        string Dynasty();

        /// <summary>
        /// 皇帝
        /// </summary>
        /// <returns></returns>
        string Emperor();
    }
}
