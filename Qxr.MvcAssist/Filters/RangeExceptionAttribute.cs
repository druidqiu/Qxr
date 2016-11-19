﻿using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Web.Mvc;

namespace Qxr.MvcAssist.Filters
{
    public class RangeExceptionAttribute : FilterAttribute, IExceptionFilter
    {
        public void OnException(ExceptionContext filterContext)
        {
            if (!filterContext.ExceptionHandled
                && filterContext.Exception is ArgumentOutOfRangeException)
            {
                int val = (int)(((ArgumentOutOfRangeException)filterContext.Exception).ActualValue);
                filterContext.Result = new ViewResult
                {
                    ViewName = "RangeError",
                    ViewData = new ViewDataDictionary(val)
                };

                filterContext.ExceptionHandled = true;
            }
        }
    }
}
