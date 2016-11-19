using Qxr.EntityFramework;
using Qxr.EntityFramework.UnitOfWork;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Qxr.Tests.Repositories
{
    public class QxrTestUow : EfUnitOfWork<QxrTestDbContext>
    {
        public QxrTestUow(IDbContextProvider<QxrTestDbContext> dbContextProvider)
            : base(dbContextProvider)
        {

        }
    }
}
