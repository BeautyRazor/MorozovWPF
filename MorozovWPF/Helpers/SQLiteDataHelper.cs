using System;
using System.Collections.Generic;
using System.Data.SQLite;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;
using MorozovWPF.Models;

namespace MorozovWPF.Helpers
{
    class SQLiteDataHelper {
        public SQLiteParameter[] Parameters { get; }
        public string FieldsList { get; }
        public string ParametersList { get; }

        public SQLiteDataHelper(object dataItem) {
            List<PropertyInfo> properties = GetProperties(dataItem);
            if (properties.Count == 0) {
                throw new ArgumentException("No any public properties with SQLiteField attribute", "dataItem");
            }
            Parameters = GetParameters(properties, dataItem);
            FieldsList = GetFieldsList(properties);
            ParametersList = GetParametersList(properties);
        }

        List<PropertyInfo> GetProperties(object obj) {
            List<PropertyInfo> result = new List<PropertyInfo>();
            var allProperties = obj.GetType().GetProperties(BindingFlags.Public | BindingFlags.Static | BindingFlags.Instance);
            result.AddRange(allProperties.Where(p => p.GetCustomAttribute<SQLiteFieldAttribute>(true) != null));
            return result.OrderBy(p => p.Name).ToList();
        }

        string GetParametersList(List<PropertyInfo> properties) {
            List<string> parameterNames = properties.Select(p => GetParameterName(p)).ToList();
            string result = string.Format("({0})", string.Join(", ", parameterNames));
            return result;
        }

        string GetFieldsList(List<PropertyInfo> properties) {
            List<string> fieldsList = properties.Select(p => GetFieldName(p)).ToList();
            string result = string.Format("({0})", string.Join(", ", fieldsList));
            return result;
        }

        string GetParameterName(PropertyInfo property) {
            return $"@{property.Name}";
        }

        string GetFieldName(PropertyInfo property) {
            SQLiteFieldAttribute sqliteAttribute = property.GetCustomAttribute<SQLiteFieldAttribute>(true);
            if (sqliteAttribute == null || string.IsNullOrEmpty(sqliteAttribute.FieldName)) {
                return property.Name;
            }
            return sqliteAttribute.FieldName;
        }

        SQLiteParameter[] GetParameters(List<PropertyInfo> properties, object obj) {
            return properties.Select(p => GetSQLiteParameter(p, obj)).ToArray();
        }

        SQLiteParameter GetSQLiteParameter(PropertyInfo property, object obj) {
            string parameterName = GetParameterName(property);
            object parameterValue = property.GetValue(obj);
            if (parameterValue != null) {
                bool convertToString = IsConvertPropertyToString(property);
                if (convertToString) {
                    parameterValue = parameterValue.ToString();
                }

                bool convertToId = IsConvertToID(property);
                if (convertToId) {
                    parameterValue = ((IConvertiblrToId) parameterValue).convertToId();
                }
            }
            return new SQLiteParameter(parameterName, parameterValue);
        }

        bool IsConvertPropertyToString(PropertyInfo property) {
            SQLiteFieldAttribute sqliteAttribute = property.GetCustomAttribute<SQLiteFieldAttribute>(true);
            if (sqliteAttribute == null) {
                return false;
            }
            return sqliteAttribute.ConvertToString;
        }
        bool IsConvertToID(PropertyInfo property) {
            SQLiteFieldAttribute sqliteAttribute = property.GetCustomAttribute<SQLiteFieldAttribute>(true);
            if (sqliteAttribute == null) {
                return false;
            }
            return sqliteAttribute.ConvertToId;
        }

    }
}
