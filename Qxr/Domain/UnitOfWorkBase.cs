namespace Qxr.Domain
{
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        public abstract bool Commit();
    }
}
