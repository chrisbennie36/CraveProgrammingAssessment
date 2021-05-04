using System;
using System.Collections;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Reflection;

namespace Infrastructure.Sql
{
    public static class IEnumerableExtensions
    {
        private static bool IsGenericList(Type type)
        {
            return type.IsGenericType && type.GetInterfaces().Contains(typeof(IEnumerable));
        }

        private static IEnumerable<PropertyInfo> GetPropertyInfo<T>()
        {
            return typeof(T).GetProperties()
                .Where(itm => !(IsGenericList(itm.PropertyType)));
        }

        private static void AddDataColumns(this DataTable dataTable, IEnumerable<PropertyInfo> propertyInfo)
        {
            if (propertyInfo == null)
            {
                throw new ArgumentNullException(nameof(propertyInfo));
            }

            foreach (var prop in propertyInfo)
            {
                var underlyingType = Nullable.GetUnderlyingType(prop.PropertyType);
                var type = underlyingType ?? prop.PropertyType;
                if (type.IsEnum)
                {
                    type = typeof(int);
                }

                dataTable.Columns.Add(new DataColumn(prop.Name, type)
                {
                    AllowDBNull = underlyingType != null || !type.IsValueType
                });
            }
        }

        public static DataTable CreateDataTable<T>(this IEnumerable<T> enumerable, string name) where T : class
        {
            if (string.IsNullOrWhiteSpace(name))
            {
                throw new ArgumentNullException(nameof(name));
            }
            if (enumerable == null)
            {
                throw new ArgumentNullException(nameof(enumerable));
            }

            var dataTable = new DataTable(name);
            //Order by name so the datatable does not have random order, which crashes dapper.
            var propertyInfo = GetPropertyInfo<T>().OrderBy(itm => itm.Name);
            dataTable.AddDataColumns(propertyInfo);

            foreach (var item in enumerable)
            {
                var row = dataTable.NewRow();
                foreach (var prop in propertyInfo)
                {
                    row[prop.Name] = prop.GetValue(item) ?? DBNull.Value;
                }

                dataTable.Rows.Add(row);
            }

            return dataTable;
        }
    }
}
