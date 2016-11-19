namespace Qxr.Models.Domain
{
    public interface IUnitOfWork
    {
        bool Commit();
    }
}
