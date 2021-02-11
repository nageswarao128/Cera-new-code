namespace CERAPlatform
{
    public interface ICeraPlatform
    {
        public object Sync();
        public object SyncTenants();
        public object SyncSubscriptions();
        public object SyncResources();
        public object SyncVMs();
        public object SyncWebApps();
        public object SyncServicePlans();
        public object SyncSqlServers();
        public object SyncSqlDbs();
    }
}
