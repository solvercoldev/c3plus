using System.Collections.Generic;

namespace Infraestructure.Data.Core.Extensions
{
    public static class ListExtensions
    {
        /// <summary>
        /// Extensor Method for translate a list into a InMemoryObjectSet.
        /// This extension method is only for testing purposed.
        /// </summary>
        /// <typeparam name="T">Typeof elements</typeparam>
        /// <param name="list">List to translate into a IObjectSet</param>
        /// <returns>InMemoryObjectSet</returns>
        [System.Diagnostics.CodeAnalysis.SuppressMessage("Microsoft.Design", "CA1002:DoNotExposeGenericLists")]
        public static MemorySet<T> ToMemorySet<T>(this List<T> list)
            where T : class
        {
            return new MemorySet<T>(list);
        }
    }
}