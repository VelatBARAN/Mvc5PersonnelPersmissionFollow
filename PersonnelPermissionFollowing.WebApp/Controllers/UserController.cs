using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using PersonnelPermissionFollowing.BusinessLayer;
using PersonnelPermissionFollowing.BusinessLayer.Results;
using PersonnelPermissionFollowing.Entities;
using PersonnelPermissionFollowing.Entities.ValueObjects;
using PersonnelPermissionFollowing.WebApp.Filters;
using PersonnelPermissionFollowing.WebApp.Models;

namespace PersonnelPermissionFollowing.WebApp.Controllers
{
    [Auth]
    public class UserController : Controller
    {
        private UserManager userManager = new UserManager();
        private AuthorityManager authorityManager = new AuthorityManager();
        private PersonnelManager personnelManager = new PersonnelManager();

        [AuthAdmin]
        public ActionResult Index()
        {
            var users = userManager.ListQueryable().Include(u => u.Authority).Include(u => u.Personnels).OrderByDescending(x => x.CreatedOnDatetime);
            return View(users.ToList());
        }

        [AuthAdmin]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = userManager.Find(x => x.Id == id.Value);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        [AuthAdmin]
        [HttpGet]
        public ActionResult Create()
        {
            UserList();
            return View();
        }

        [AuthAdmin]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Users users)
        {
            ModelState.Remove("CreatedOnDatetime");
            ModelState.Remove("ModifiedOnDatetime");
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                BusinessLayerResult<Users> res = userManager.InsertUser(users);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    UserEditCreateList(users);
                    return View(users);
                }

                return RedirectToAction("Index");
            }

            UserEditCreateList(users);
            return View(users);
        }

        [AuthAdmin]
        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = userManager.Find(x => x.Id == id.Value);
            if (users == null)
            {
                return HttpNotFound();
            }
            UserEditCreateList(users);
            return View(users);
        }

        [AuthAdmin]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Users users)
        {
            ModelState.Remove("ModifiedOnDatetime");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                BusinessLayerResult<Users> res = userManager.UpdateUser(users);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    UserEditCreateList(users);
                    return View(users);
                }

                return RedirectToAction("Index");
            }
            UserList();
            return View(users);
        }

        [AuthAdmin]
        public ActionResult Delete(int? id)
        {
            Users user = userManager.Find(x => x.Id == id.Value);
            var name = user.Personnels.Name + ' ' + user.Personnels.Surname;
            if (user != null)
            {
                int res = userManager.Delete(user);
                if (res > 0)
                {
                    return Json(new { hasError = false, Message = $"{ name }" + " adlı kullanıcı başarılı bir şekilde silindi." }, JsonRequestBehavior.AllowGet);
                    //return Json(new { hasError = false, Massage = "Personel başarılı bir şekilde silindi." });
                }
                else
                {
                    return Json(new { hasError = true, Message = $"{ name }" + " adlı kullanıcı silinirken hata oluştu." }, JsonRequestBehavior.AllowGet);
                    //return Json(new { hasError = true, Message = "Personel silinirken hata oluştu." });
                }
            }
            return Json(new { results = true, Message = $"{ name }" + " adlı bir kullanıcı bulunamadı." }, JsonRequestBehavior.AllowGet);
        }

        private void UserList()
        {
            List<SelectListItem> result = (from k in personnelManager.List()
                                           where k.ExitOfJobDatetime == null
                                           select new SelectListItem
                                           {
                                               Text = k.Tc + " " + k.Name + " " + k.Surname,
                                               Value = k.Id.ToString()
                                           }).ToList();
            ViewBag.PersonnelsId = result;
            ViewBag.AuthorityId = new SelectList(authorityManager.List(), "Id", "Name");
            //ViewBag.PersonnelsId = new SelectList(personnelManager.List(), "Id", "Tc");
        }

        private void UserEditCreateList(Users user)
        {
            List<SelectListItem> result = (from k in personnelManager.List()
                                           where k.ExitOfJobDatetime == null
                                           select new SelectListItem
                                           {
                                               Text = k.Tc + " " + k.Name + " " + k.Surname,
                                               Value = k.Id.ToString()
                                           }).ToList();

            ViewBag.PersonnelsId = result;
            ViewBag.AuthorityId = new SelectList(authorityManager.List(), "Id", "Name", user.AuthorityId);
            //ViewBag.PersonnelsId = new SelectList(personnelManager.List(), "Id", "Tc");
        }

        private void UserProfileEditCreateList(Users user)
        {
            List<SelectListItem> result = (from k in personnelManager.List()
                                           where k.ExitOfJobDatetime == null && k.Id == user.Personnels.Id
                                           select new SelectListItem
                                           {
                                               Text = k.Tc + " " + k.Name + " " + k.Surname,
                                               Value = k.Id.ToString()
                                           }).ToList();
            ViewBag.PersonnelsId = result;
            ViewBag.AuthorityId = new SelectList(authorityManager.List().Where(x => x.Id == user.Authority.Id), "Id", "Name", user.AuthorityId);
            //ViewBag.PersonnelsId = new SelectList(personnelManager.List(), "Id", "Tc");
        }

        [HttpGet]
        public ActionResult ShowUserProfile()
        {
            BusinessLayerResult<Users> res = userManager.ShowUserProfileById(CurrentSession.User.Id);
            if (res.Errors.Count > 0)
            {
                res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                return View("ShowUserProfile", res.Result);
            }

            return View("ShowUserProfile", res.Result);
        }

        [HttpGet]
        public ActionResult EditUserProfile()
        {
            BusinessLayerResult<Users> res = userManager.ShowUserProfileById(CurrentSession.User.Id);
            if (res.Errors.Count > 0)
            {
                res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                return View("ShowUserProfile", res.Result);
            }

            UserProfileEditCreateList(res.Result);
            return View(res.Result);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult EditUserProfile(Users users)
        {
            ModelState.Remove("ModifiedOnDatetime");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                BusinessLayerResult<Users> res = userManager.UpdateUser(users);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    UserProfileEditCreateList(users);
                    return View(res.Result);
                }

                CurrentSession.Set<Users>("login", res.Result);
                return RedirectToAction("ShowUserProfile", res.Result);
            }
            UserProfileEditCreateList(users);
            return View(users);
        }

        public ActionResult DetailsUserProfile()
        {
            BusinessLayerResult<Users> res = userManager.ShowUserProfileById(CurrentSession.User.Id);
            if (res.Errors.Count > 0)
            {
                res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                return View("DetailsUserProfile", res.Result);
            }

            return View("DetailsUserProfile", res.Result);
        }

        public ActionResult UserChangePassword()
        {

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult UserChangePassword(ChangePasswordViewModel model)
        {
            ModelState.Remove("ModifiedOnDatetime");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                BusinessLayerResult<Users> res = userManager.ChangePassword(model);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    return View(res.Result);
                }

                ViewBag.ChangePassword = "Şifreniz başarılı bir şekilde değiştirilmiştir";
                return View(model);
            }

            return View(model);
        }
    }
}
