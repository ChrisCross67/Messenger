using System;
using System.Collections.Generic;

namespace Messenger.Extensions
{
    public static class EnumerableExtensions
    {
        /// <summary>
        /// Shortcut for foreach
        /// </summary>
        /// <typeparam name="T"></typeparam>
        /// <param name="source">The source.</param>
        /// <param name="action">The action.</param>
        public static void ForEach<T>(this IEnumerable<T> source, Action<T> action)
        {
            foreach (var item in source)
                action(item);
        }

    }
}
