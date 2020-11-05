using PersonnelPermissionFollowing.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonnelPermissionFollowing.WebApp.Filters
{
    public class AuthAdmin : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if (CurrentSession.User != null && CurrentSession.User.Authority.Name != "Admin")
            {
                filterContext.Result = new RedirectResult("/Home/AccessDenied");
            }
        }
    }
}