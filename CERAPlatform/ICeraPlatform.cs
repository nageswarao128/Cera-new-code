using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace CERAPlatform
{
    public interface ICeraPlatform
    {
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
