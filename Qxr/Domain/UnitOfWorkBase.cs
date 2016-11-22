namespace Qxr.Domain
{
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        public abstract void BeginUow();
        public abstract bool Commit();
    }
}
