
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using ChangTing.UI.WebProxy;

namespace  ChangTing.UI.Models
{
    /// <summary>
    /// 回话状态
    /// </summary>
    [AttributeUsage(AttributeTargets.Class | AttributeTargets.Method, AllowMultiple = true, Inherited = true)]
    public class LoginCheckAttribute : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (filterContext.IsChildAction)
            {
                return;
            }
            object sInfo = UserState.GetUserState();
            if (sInfo == null)
            {
                filterContext.Result = new RedirectResult("/UI/index");
            }
        }
    }
}
