namespace Qxr.Repositories.Infrastructures
{
    public static class DataContextFactory
    {
        private static IDataStorage _dataContextStorageContainer;
        private const string StorageKey = "DataContextStorageContainer";

        public static void Initialize(IDataStorage dataContextStorageContainer)
        {
            _dataContextStorageContainer = dataContextStorageContainer;
        }

        public static QxrDataContext GetDataContext()
        {
            QxrDataContext dataContext = _dataContextStorageContainer.Retrieve<QxrDataContext>(StorageKey);
            if (dataContext == null)
            {
                dataContext = new QxrDataContext();
                _dataContextStorageContainer.Store(StorageKey, dataContext);
            }

            return dataContext;
        }

        public static QxrDataContext ResetDataContent()
        {
            var dataContext = new QxrDataContext();
            _dataContextStorageContainer.Store(StorageKey, dataContext);

            return dataContext;
        }
    }
}
