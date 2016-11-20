using System.Data.Entity;

namespace Qxr.EntityFramework.Infrastructures
{
    public interface IDbContextProvider<out TDbContext>
        where TDbContext: DbContext
    {
        TDbContext DbContext { get;}
        void ResetDbContext();
    }
}
