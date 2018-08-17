using System;
using System.Collections.Generic;
using System.Text;

namespace Core.Interfaces.Managers
{
    public interface IDataUpdateManager<TSource> where TSource : class
    {
        /// <summary>
        /// Enumeration items from the updated collection that are not in the existing collection.
        /// </summary>
        IEnumerable<TSource> NewItems { get; }
        /// <summary>
        /// Enumeration items from the existing collection that are not in the updating collection.
        /// </summary>
        IEnumerable<TSource> NonActualItems { get; }

        /// <summary>
        /// Updating the existing enumeration by dividing it by "New" and "NonActual" components.
        /// </summary>
        /// <param name="updatedEnum">The updated enumeration for updating the existing enumeration.</param>
        /// <param name="existingEnum">The existings enumeration.</param>
        void Update(IEnumerable<TSource> updatedEnum, IEnumerable<TSource> existingEnum);
    }
}
