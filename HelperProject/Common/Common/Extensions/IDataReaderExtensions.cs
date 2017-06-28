using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Text;

namespace Common
{
    public static class IDataReaderExtensions
    {
        public static T GetValue<T>(this IDataReader reader, int index)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            if (reader.IsDBNull(index))
                return default(T);
            object objectValue = reader[index];
            if (typeof(T) == typeof(Guid) || typeof(T) == typeof(Guid?))
            {
                byte[] byteArray = objectValue as byte[];
                if (byteArray != null)
                    return (T)(object)new Guid(byteArray);
                throw new InvalidCastException(string.Format("Unable to read and convert database value from {0} to Guid", (objectValue != null ? objectValue.GetType().Name : "null")));
            }
            Type convertibleType = Nullable.GetUnderlyingType(typeof(T)) ?? typeof(T);
            T value = (T)Convert.ChangeType(objectValue, convertibleType);
            if (typeof(T) == typeof(string))
            {
                string stringValue = (string)(object)value;
                if (!string.IsNullOrEmpty(stringValue))
                    value = (T)(object)(stringValue.TrimEnd());
            }
            return value;
        }

        public static T GetValue<T>(this IDataReader reader, string columnName)
        {
            if (reader == null)
                throw new ArgumentNullException("reader");
            int index = reader.GetOrdinal(columnName);
            return GetValue<T>(reader, index);
        }
    }
}
