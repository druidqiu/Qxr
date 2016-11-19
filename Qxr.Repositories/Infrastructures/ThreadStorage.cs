using System.Collections;

namespace Qxr.Repositories.Infrastructures
{
    public class ThreadStorage : IDataStorage
    {
        private static readonly Hashtable HsTable = new Hashtable();

        public T Retrieve<T>(string storageKey)
        {
            if (HsTable.Contains(storageKey))
                return (T) HsTable[storageKey];
            return default(T);
        }

        public void Store<T>(string storageKey, T entity)
        {
            if (HsTable.Contains(storageKey))
                HsTable[storageKey] = entity;
            else
                HsTable.Add(storageKey, entity);
        }
    }
}
