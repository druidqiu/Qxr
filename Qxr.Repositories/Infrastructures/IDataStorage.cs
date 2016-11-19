namespace Qxr.Repositories.Infrastructures
{
    public interface IDataStorage
    {
        T Retrieve<T>(string storageKey);
        void Store<T>(string storageKey, T entity);
    }
}
