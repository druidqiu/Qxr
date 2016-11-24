using System;

namespace Qxr.Dependency
{
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method)]
    public class DisableAuditingAttribute : Attribute
    {
    }
}
