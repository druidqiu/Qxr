using System;
using System.Collections.Generic;
using System.Linq;
using System.Reflection;
using System.Text;
using System.Threading.Tasks;

namespace Qxr.Reflection.Extentsions
{
    public static class MemberInfoExtensions
    {
        public static T GetSingleAttributeOrNull<T>(this MemberInfo memberInfo, bool inherit = true) where T : class
        {
            if (memberInfo == null)
            {
                throw new ArgumentNullException("memberInfo");
            }

            var attrs = memberInfo.GetCustomAttributes(typeof(T), inherit);
            if (attrs.Length > 0)
            {
                return (T)attrs[0];
            }

            return default(T);
        }
    }
}
