using System;
using System.Collections.Generic;
using System.Linq;
using System.Linq.Dynamic.Core;
using CourseLibrary.API.Services;

namespace CourseLibrary.API.Helpers
{
    public static class IQueryablyExtensions
    {
        public static IQueryable<T> ApplySort<T>(this IQueryable<T> source, string orderBy,
            Dictionary<string, PropertyMappingValue> mappingDictionary)
        {
            if (source == null)
            {
                throw new ArgumentNullException(nameof(source));
            }

            if (mappingDictionary == null)
            {
                throw new ArgumentNullException(nameof(mappingDictionary));
            }

            if (string.IsNullOrWhiteSpace(orderBy))
            {
                return source;
            }

            var orderByString = string.Empty;

            var orderByAfterSplit = orderBy.Split(',');
            foreach (var orderByClause in orderByAfterSplit.Reverse())
            {
                var trimmedOrderbyClause = orderByClause.Trim();

                var orderDescending = trimmedOrderbyClause.EndsWith(" desc");

                var indexOfFirstSpace = trimmedOrderbyClause.IndexOf(" ");
                var propertyName = indexOfFirstSpace == -1
                    ? trimmedOrderbyClause
                    : trimmedOrderbyClause.Remove(indexOfFirstSpace);

                if (!mappingDictionary.ContainsKey(propertyName))
                {
                    throw new ArgumentException($"Key mapping for {propertyName} is missing");
                }

                var propertyMappingValue = mappingDictionary[propertyName];

                if (propertyMappingValue == null)
                {
                    throw new ArgumentException("propertyMappingValue");
                }

                foreach (var destinationProperty in propertyMappingValue.DestinationProperties)
                {
                    if (propertyMappingValue.Revert)
                    {
                        orderDescending = !orderDescending;
                    }

                    orderByString = orderByString +
                                        (string.IsNullOrWhiteSpace(orderByString) ? string.Empty : ", ")
                                        + destinationProperty
                                        + (orderDescending ? " descending" : " ascending");
                }
            }

            return source.OrderBy(orderByString);
        }
    }
}
