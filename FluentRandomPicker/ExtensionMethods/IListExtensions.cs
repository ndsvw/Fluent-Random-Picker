using System;
using System.Collections.Generic;

namespace FluentRandomPicker.ExtensionMethods
{
    /// <summary>
    /// Extension methods for IList.
    /// </summary>
    internal static class IListExtensions
    {
        /// <summary>
        /// Adds multiple elements to the list.
        /// </summary>
        /// <param name="list">The list.</param>
        /// <param name="items">The items to add to the list.</param>
        /// <typeparam name="T">The type of the elements in the list.</typeparam>
        public static void AddRange<T>(this IList<T> list, IEnumerable<T> items)
        {
            if (items == null)
                throw new ArgumentNullException(nameof(items));

            if (list == null)
                throw new ArgumentNullException(nameof(list));

            if (list is List<T> asList)
            {
                asList.AddRange(items);
                return;
            }

            foreach (var item in items)
            {
                list.Add(item);
            }
        }
    }
}