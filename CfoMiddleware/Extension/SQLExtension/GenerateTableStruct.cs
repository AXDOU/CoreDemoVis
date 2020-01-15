
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Text;
using System.Data;
using System.Reflection;
using Cfo.DTO.Base.Extension.SqlTableStruct;
using System.ComponentModel.DataAnnotations;
using SqlSugar;
using System.Linq.Expressions;

namespace CfoMiddleware.Extension
{
    /// <summary>
    /// 生成表结构
    /// </summary>
    public static class GenerateTableStruct
    {
        private static int defaultSize { get; set; }

        public static string GetCustomDesc<T>(this T data, string column)
        {
            Type type = typeof(T);
            return type.GetProperties().FirstOrDefault(x => x.Name.Equals(column)).GetCustomAttribute<CustomDescAttribute>()?.Descroption;
        }

        /// <summary>
        /// 简单生成SQL表结构语句
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="data"></param>
        /// <returns></returns>
        public static string ToSqlTableStruct<T>(this T data, bool isDescription = true)
        {
            Type type = typeof(T);
            AttributeExtension attribute = new AttributeExtension(defaultSize);
            List<SqlAttributeTable> sqlAttributes = attribute.ToSqlAttributeTables(type.GetProperties());
            //如果表已存在，先删除旧表

            //IF  OBJECT_ID('tb_post') IS  NOT null
            string str = $"IF  OBJECT_ID('{type.Name}') IS  NOT null  begin drop table {type.Name} end {Environment.NewLine}";
            str += $"Create Table {type.Name}" + Environment.NewLine;
            str += $"({Environment.NewLine}";
            foreach (var item in sqlAttributes)
            {
                str += $"{item.ColumnName} {item.PropertyName}";
                if (item.IsPrimaryKey)
                    str += " PRIMARY KEY ";
                else if (sqlAttributes.FirstOrDefault() == item && item.ColumnName.ToUpper().IndexOf("ID") > -1)
                    str += " PRIMARY KEY ";
                //str += $"{(item.IsPrimaryKey ? " PRIMARY KEY " : "")}";
                str += $"{(item.IsIdentity && item.ColumnType == typeof(int) ? " IDENTITY " : "")}";
                str += $" {(item.IsGenericType ? "NULL" : "NOT NULL")}";
                str += $",{Environment.NewLine}";
            }
            str += $"){Environment.NewLine}";
            str += "GO";
            if (isDescription)
                str += $"{Environment.NewLine}" + GetSqlDescription(sqlAttributes, type.Name);
            return str;
        }

        private static string GetSqlDescription(List<SqlAttributeTable> sqlAttributes, string tableName)
        {
            string sqldescription = string.Empty;
            foreach (var item in sqlAttributes)
            {
                //EXEC sys.sp_addextendedproperty N'MS_Description',N'名称',N'SCHEMA',N'dbo',N'TABLE',N'tb_user',N'COLUMN',N'RealName' GO
                sqldescription += $"EXEC sys.sp_addextendedproperty N'MS_Description',N'{item.Description}',N'SCHEMA',N'dbo',N'TABLE',N'{tableName}',N'COLUMN',N'{item.ColumnName}' GO" + Environment.NewLine;
            }
            return sqldescription;
        }


        public static T DefaultStrSize<T>(this T data, int size)
        {
            defaultSize = size;
            return data;
        }
    }



}