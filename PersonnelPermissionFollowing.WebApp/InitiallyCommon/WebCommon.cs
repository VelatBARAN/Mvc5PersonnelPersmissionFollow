using PersonnelPermissionFollowing.Common;
using PersonnelPermissionFollowing.Entities;
using PersonnelPermissionFollowing.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace PersonnelPermissionFollowing.WebApp.InitiallyCommon
{
    public class WebCommon : ICommon
    {
        public string GetUsername()
        {
            Users user = CurrentSession.User;

            if (user != null)
                return user.Username;
            else
                return "system";
        }
    }
}