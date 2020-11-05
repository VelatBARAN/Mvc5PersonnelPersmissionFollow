using PersonnelPermissionFollowing.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace PersonnelPermissionFollowing.WebApp.Filters
{
    public class AuthAdminOrKoordinator : FilterAttribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationContext filterContext)
        {
            if(CurrentSession.User != null && CurrentSession.User.AuthorityId == 2 )
            {
                filterContext.Result = new RedirectResult("/Home/AccessDenied");
            }
        }
    }
}