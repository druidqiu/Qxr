using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qxr.Domain
{
    public abstract class UnitOfWorkBase : IUnitOfWork
    {
        public abstract bool Commit();
    }
}
