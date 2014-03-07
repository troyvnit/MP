namespace MP.Data.Infrastructure
{
    public class DatabaseFactory : Disposable, IDatabaseFactory
    {
        private MPEntities _dataContext;
        public MPEntities Get()
        {
            return _dataContext ?? (_dataContext = new MPEntities());
        }
        protected override void DisposeCore()
        {
            if (_dataContext != null)
                _dataContext.Dispose();
        }
    }
}
