using Microsoft.Data.SqlClient;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CodeBuns.Dotnet.Common.Helpers
{
    public class AdoNetHelper
    {
        public static T GetColumnValue<T>(DataRow dr, string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("Column name is empty.");

            if (dr == null || dr[columnName] == DBNull.Value)
            {
                if (typeof(T) == typeof(string))
                    return (T)(object)string.Empty;

                return default;
            }

            var targetType = typeof(T);

            if (Nullable.GetUnderlyingType(targetType) != null)
            {
                if (dr[columnName] == null || dr[columnName] == DBNull.Value)
                    return default;

                targetType = Nullable.GetUnderlyingType(targetType);
            }

            return (T)Convert.ChangeType(dr[columnName], targetType);
        }

        public static T GetColumnValue<T>(SqlDataReader reader, string columnName)
        {
            if (string.IsNullOrEmpty(columnName))
                throw new ArgumentNullException("Column name is empty.");

            if (reader[columnName] == DBNull.Value)
            {
                if (typeof(T) == typeof(string))
                    return (T)(object)string.Empty;
                return default;
            }

            var targetType = typeof(T);

            if (Nullable.GetUnderlyingType(targetType) != null)
            {
                if (reader[columnName] == DBNull.Value)
                    return default;

                targetType = Nullable.GetUnderlyingType(targetType);
            }

            return (T)Convert.ChangeType(reader[columnName], targetType);
        }
    }
}
