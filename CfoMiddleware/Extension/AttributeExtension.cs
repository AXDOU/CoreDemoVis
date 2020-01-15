
using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.IO;
using System.Linq;
using System.Reflection;
using System.Text;
using Cfo.DTO.Base.Extension.SqlTableStruct;
using SqlSugar;

namespace CfoMiddleware.Extension
{
    public class AttributeExtension
    {

        private int _defaultStrSize { get; set; }

        public AttributeExtension(int defaultStrSize)
        {
            this._defaultStrSize = defaultStrSize;
        }

        private Dictionary<Type, string> propertyDics { get { return this.GetPropertyDics(); } }

        public List<SqlAttributeTable> ToSqlAttributeTables(PropertyInfo[] properties)
        {
            List<SqlAttributeTable> sqlAttributes = properties.Select(x => GetSqlAttributeTable(x)).ToList();
            return sqlAttributes;
        }

        private SqlAttributeTable GetSqlAttributeTable(PropertyInfo type)
        {
            // ? "NULL" : "NOT NULL"
            var IsIdentity = type.GetCustomAttributes<SugarColumn>().FirstOrDefault()?.IsIdentity ?? false;
            string Description = type.GetCustomAttribute<SugarColumn>()?.ColumnDescription;
         
            string ColumnType = propertyDics.FirstOrDefault(x => x.Key.Equals(type.PropertyType)).Value;
            string defaultStr = $" NVARCHAR({_defaultStrSize}) ";
            SqlAttributeTable sqlAttribute = new SqlAttributeTable
            {
                IsGenericType = type.PropertyType.IsGenericType,
                IsIdentity = type.GetCustomAttributes<SugarColumn>().FirstOrDefault()?.IsIdentity ?? false,
                IsPrimaryKey = type.GetCustomAttributes<SugarColumn>().FirstOrDefault()?.IsPrimaryKey ?? false,
                ColumnName = type.Name,
                ColumnType = type.PropertyType,
                PropertyName = $"{(ColumnType ?? defaultStr)}",
                Description = Description

            };
            return sqlAttribute;
        }

        private Dictionary<Type, string> GetPropertyDics()
        {
            Dictionary<Type, string> valuePairs = new Dictionary<Type, string>();
            valuePairs.Add(typeof(int), "INT");
            valuePairs.Add(typeof(int?), "INT");
            valuePairs.Add(typeof(string), $"NVARCHAR({_defaultStrSize})");
            valuePairs.Add(typeof(DateTime), "DATETIME");
            valuePairs.Add(typeof(DateTime?), "DATETIME");
            valuePairs.Add(typeof(bool), "bit");
            valuePairs.Add(typeof(bool?), "bit");
            valuePairs.Add(typeof(Double), "FLOAT");
            valuePairs.Add(typeof(Double?), "FLOAT");
            valuePairs.Add(typeof(Guid), "UNiqueidentifier");
            valuePairs.Add(typeof(Guid?), "UNiqueidentifier");
            valuePairs.Add(typeof(Decimal), "DECIMAL(18,2)");
            valuePairs.Add(typeof(Decimal?), "DECIMAL(18,2)");
            return valuePairs;
        }
    }
}