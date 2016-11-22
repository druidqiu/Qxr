using System;
using Qxr.Domain;

namespace Qxr.EntityFramework.Repositories
{
    [AttributeUsage(AttributeTargets.Class)]
    public class AutoRepositoryTypesAttribute : Attribute
    {
        public static AutoRepositoryTypesAttribute Default { get; private set; }

        public Type RepositoryInterface { get; private set; }
        public Type RepositoryImplementation { get; private set; }

        static AutoRepositoryTypesAttribute()
        {
            Default = new AutoRepositoryTypesAttribute(typeof (IRepository<>), typeof (EfRepositoryBase<,>));
        }

        public AutoRepositoryTypesAttribute(Type repositoryInterface, Type repositoryImplementation)
        {
            RepositoryInterface = repositoryInterface;
            RepositoryImplementation = repositoryImplementation;
        }
    }
}
