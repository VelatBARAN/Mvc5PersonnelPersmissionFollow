using PersonnelPermissionFollowing.BusinessLayer;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PersonnelPermissionFollowing.BusinessLayer.Results;
using PersonnelPermissionFollowing.WebApp.Models;
using PersonnelPermissionFollowing.Entities.ValueObjects;
using PersonnelPermissionFollowing.Entities;
using PersonnelPermissionFollowing.WebApp.Filters;
using System.Data.Entity;
using PersonnelPermissionFollowing.Entities.Messages;

namespace PersonnelPermissionFollowing.WebApp.Controllers
{
    public class HomeController : Controller
    {
        private UserManager userManager = new UserManager();
        private PersonnelPermissionRequestManager personnelPermissionRequestManager = new PersonnelPermissionRequestManager();
        private PersonnelManager personnelManager = new PersonnelManager();
        private ZoneManager zoneManager = new ZoneManager();

        [Auth]
        public ActionResult Index()
        {
            ViewBag.TotalPersonnel = personnelManager.ListQueryable()
                                             .Include("PersonnelDegrees").Include("PersonnelPositions").Include("PersonnelTasks")
                                             .Where(m => m.ExitOfJobDatetime == null).Count();

            ViewBag.TotalPersonnelPermission = personnelPermissionRequestManager.ListQueryable()
                                                             .Include("Personnels")
                                                             .Include("PersonnelPermissionTips")
                                                             .Include("PermissionStates")
                                                             .Include("Users")
                                                             .Where(x=>x.PermissionEndDatetime >= DateTime.Today && x.PersonnelPermissionTipsId == 2 && x.PermissionStatesId == 2).Count();

            ViewBag.TotalPersonneLReport = personnelPermissionRequestManager.ListQueryable()
                                                 .Include("Personnels")
                                                 .Include("PersonnelPermissionTips")
                                                 .Include("PermissionStates")
                                                 .Include("Users")
                                                 .Where(x => x.PermissionEndDatetime >= DateTime.Today && ( x.PersonnelPermissionTipsId == 1 || 
                                                        x.PersonnelPermissionTipsId == 3 || x.PersonnelPermissionTipsId == 4) && x.PermissionStatesId == 2).Count();

            ViewBag.TotalZone = zoneManager.ListQueryable()
                                                   .Include("Personnels")
                                                   .Where(m => m.Personnels.ExitOfJobDatetime == null).Count();

            return View();
        }

        [HttpGet]
        public ActionResult Login()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Login(LoginViewModel model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Users> res = userManager.LoginUser(model);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    //foreach (var item in res.Errors)
                    //{
                    //    if (item.Code == ErrorMessageCode.PersonnelIsGivedWorkExit)
                    //        ViewBag.PersonnelIsGivedWorkExit = item.Message;
                    //}
                    return View(model);
                }

                CurrentSession.Set<Users>("login", res.Result);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public ActionResult LogOut()
        {
            Session.Clear();
            return RedirectToAction("Login");
        }

        [HttpGet]
        public ActionResult ForgetPassword()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult ForgetPassword(ForgetPasswordViewModel model)
        {
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Users> res = userManager.ForgetPasswordUser(model);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(model);
                }

                ViewBag.Info = "Şifre başarılı bir şekilde gönderildi. Lütfen e-posta adresinizi kontrol ediniz..";
                return View("ForgetPassword");
            }

            return View(model);
        }
        public ActionResult AccessDenied()
        {
            return View();
        }
    }
}