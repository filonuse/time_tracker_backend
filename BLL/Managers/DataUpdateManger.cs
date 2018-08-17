using Core.Interfaces.Managers;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;

namespace BLL.Managers
{
    public class DataUpdateManager<TSource> : IDataUpdateManager<TSource> where TSource : class
    {
        public DataUpdateManager() { }

        public IEnumerable<TSource> NewItems { get; private set; }
        public IEnumerable<TSource> NonActualItems { get; private set; }

        public void Update(IEnumerable<TSource> updatedEnum, IEnumerable<TSource> existingEnum)
        {
            NewItems = updatedEnum.Except(existingEnum);
            NonActualItems = existingEnum.Except(updatedEnum);
        }
    }
}
