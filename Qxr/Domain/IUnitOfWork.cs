namespace Qxr.Domain
{
    public interface IUnitOfWork
    {
        bool Commit();
    }
}
