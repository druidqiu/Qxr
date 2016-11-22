namespace Qxr.Domain
{
    public interface IUnitOfWork
    {
        void BeginUow();
        bool Commit();
    }
}
