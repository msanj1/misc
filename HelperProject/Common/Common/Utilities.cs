using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;
using System.Text;
using Common;

namespace Common.Utils
{
    public class Utilities
    {
        public static void CopyFromTo<F, T>(F From, T To)
        {
            BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            Type FromType = From.GetType();
            Type ToType = To.GetType();

            PropertyInfo[] FromProperties = FromType.GetProperties(flags);
            PropertyInfo[] ToProperties = ToType.GetProperties(flags);

            foreach (PropertyInfo FromProperty in FromProperties)
            {
                PropertyInfo ToProperty = ToProperties.FirstOrDefault((p) => string.Compare(p.Name, FromProperty.Name, StringComparison.OrdinalIgnoreCase) == 0 && p.CanWrite);
                if (ToProperty != null)
                {
                    ToProperty.SetValue(To, FromProperty.GetValue(From, null), null);
                }
            }
        }
        public static void CopyRecordCollectionToList<F, T>(IEnumerable<F> FromList, List<T> ToList)
            where T : new()
        {
            foreach (F From in FromList)
            {
                T to = new T();
                CopyFromTo(From, to);
                ToList.Add(to);
            }
        }
        public static void CopyDataReaderToClass<T>(IDataReader DbReader, T dbclass)
        {
            int Columns = DbReader.FieldCount;
            BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            Type classType = dbclass.GetType();
            PropertyInfo[] Properties = classType.GetProperties(flags);
            for (int i = 0; i < Columns; i++)
            {
                string columnName = DbReader.GetName(i);
                foreach (PropertyInfo classProperty in Properties)
                {
                    if (classProperty.CanWrite && string.Compare(classProperty.Name, columnName, StringComparison.OrdinalIgnoreCase) == 0)
                    {
                        if (classProperty.PropertyType == typeof(int))
                        {
                            classProperty.SetValue(dbclass, DbReader.GetValue<int>(i), null);
                        }
                        if (classProperty.PropertyType == typeof(int?))
                        {
                            classProperty.SetValue(dbclass, DbReader.GetValue<int?>(i), null);
                        }

                        if (classProperty.PropertyType == typeof(double))
                        {
                            classProperty.SetValue(dbclass, DbReader.GetValue<double>(i), null);
                        }
                        if (classProperty.PropertyType == typeof(double?))
                        {
                            classProperty.SetValue(dbclass, DbReader.GetValue<double?>(i), null);
                        }
                        if (classProperty.PropertyType == typeof(DateTime))
                        {
                            classProperty.SetValue(dbclass, DbReader.GetValue<DateTime>(i), null);
                        }
                        if (classProperty.PropertyType == typeof(DateTime?))
                        {
                            classProperty.SetValue(dbclass, DbReader.GetValue<DateTime?>(i), null);
                        }
                        if (classProperty.PropertyType == typeof(string))
                        {
                            classProperty.SetValue(dbclass, DbReader.GetValue<string>(i), null);
                        }
                        //DbReader.GetValue<classProperty.DeclaringType>(i);
                        //object value = DbReader.getv(i);


                        break;
                    }
                }
            }
        }
        public static void CopyToFrom<F, T>(F From, T To)
        {
            BindingFlags flags = BindingFlags.Public | BindingFlags.Instance;
            Type FromType = From.GetType();
            Type ToType = To.GetType();

            PropertyInfo[] FromProperties = FromType.GetProperties(flags);
            PropertyInfo[] ToProperties = ToType.GetProperties(flags);

            foreach (PropertyInfo FromProperty in FromProperties)
            {
                PropertyInfo ToProperty = ToProperties.FirstOrDefault((p) => string.Compare(p.Name, FromProperty.Name, StringComparison.OrdinalIgnoreCase) == 0 && p.CanRead);
                if (ToProperty != null && FromProperty.CanWrite)
                {
                    FromProperty.SetValue(From, ToProperty.GetValue(To, null), null);
                }
            }
        }

    }
}
