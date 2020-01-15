
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;

namespace Cfo.DTO.Base.Extension.SqlTableStruct
{
    /// <summary>
    /// SQL表结构属性
    /// </summary>
    public class SqlAttributeTable
    {
        /// <summary>
        /// 是否主键
        /// </summary>
        public bool IsPrimaryKey { get; set; }

        /// <summary>
        /// 是否自增
        /// </summary>
        public bool IsIdentity { get; set; }

        /// <summary>
        /// 属性类型
        /// </summary>
        public Type ColumnType { get; set; }

        /// <summary>
        /// 是否可空
        /// </summary>
        public bool IsGenericType { get; set; }

        /// <summary>
        /// 列属性
        /// </summary>
        public string ColumnName { get; set; }

        /// <summary>
        /// 属性名 如 Nvarchar
        /// </summary>
        public string PropertyName { get; set; }

        /// <summary>
        /// 描述
        /// </summary>
        public string Description { get; set; }
    }
}