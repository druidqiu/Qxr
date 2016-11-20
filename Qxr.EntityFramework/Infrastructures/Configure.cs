using System.Data.Entity;

namespace Qxr.EntityFramework.Infrastructures
{
    public class Configure : DbConfiguration
    {
        public Configure()
        {
            AddInterceptor(new CommandInterceptor());
        }
    }
}
