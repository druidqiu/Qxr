using System.Data.Entity;

namespace Qxr.Repositories.Infrastructures
{
    public class EF6CodeConfig : DbConfiguration
    {
        public EF6CodeConfig()
        {
            AddInterceptor(new EFCommandInterceptor());
            SetDatabaseInitializer(new QxrDataInitializer());
        }
    }
}
