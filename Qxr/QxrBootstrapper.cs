using System;
using Qxr.Dependency;
using Qxr.Modules;
using Qxr.Dependency.Installers;

namespace Qxr
{
    public class QxrBootstrapper : IDisposable
    {
        protected bool IsDisposed;
        public IIocManager IocManager { get; private set; }
        private IQxrModuleManager _moduleManager;

        //public QxrBootstrapper()
        //    : this(Dependency.IocManager.Instance)
        //{

        //}

        public QxrBootstrapper(IIocManager iocManager)
        {
            IocManager = iocManager;
        }

        public virtual void Initialize()
        {
            new QxrCoreInstaller(IocManager).Instal();
            _moduleManager = IocManager.Resolve<IQxrModuleManager>();
            _moduleManager.InitializeModules();
        }

        public virtual void Dispose()
        {
            if (IsDisposed)
            {
                return;
            }

            IsDisposed = true;

            if (_moduleManager != null)
            {
                _moduleManager.ShutdownModules();
            }
        }
    }
}
