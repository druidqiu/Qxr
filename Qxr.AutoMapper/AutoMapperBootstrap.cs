using System.Reflection;
using Qxr.Reflection;

namespace Qxr.AutoMapper
{
    public class AutoMapperBootstrap
    {
        private readonly ITypeFinder _typeFinder;

        private static bool _createdMappingsBefore;
        private static readonly object _syncObj = new object();

        public AutoMapperBootstrap(ITypeFinder typeFinder)
        {
            _typeFinder = typeFinder;
        }

        public void CreateMappings()
        {
            lock (_syncObj)
            {
                if (_createdMappingsBefore)
                {
                    return;
                }

                FindAndAutoMapTypes();
                CreateOtherMappings();

                _createdMappingsBefore = true;
            }
        }

        private void FindAndAutoMapTypes()
        {
            var types = _typeFinder.Find(type =>
                type.IsDefined(typeof(AutoMapAttribute)) ||
                type.IsDefined(typeof(AutoMapFromAttribute)) ||
                type.IsDefined(typeof(AutoMapToAttribute))
                );

            foreach (var type in types)
            {
                AutoMapperHelper.CreateMap(type);
            }
        }

        private void CreateOtherMappings()
        {
            
        }
    }
}
