using PersonnelPermissionFollowing.BusinessLayer;
using PersonnelPermissionFollowing.BusinessLayer.Results;
using PersonnelPermissionFollowing.Entities;
using PersonnelPermissionFollowing.WebApp.Filters;
using PersonnelPermissionFollowing.WebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;
using System.Web.UI;

namespace PersonnelPermissionFollowing.WebApp.Controllers
{
    [Auth]
    public class PersonnelController : Controller
    {
        private PersonnelDegreeManager personnelDegreeManager = new PersonnelDegreeManager();
        private PersonnelPositionManager personnelPositionManager = new PersonnelPositionManager();
        private PersonnelTaskManager personnelTaskManager = new PersonnelTaskManager();
        private PersonnelPermissionRequestManager personnelPermissionRequestManager = new PersonnelPermissionRequestManager();
        private PersonnelManager personnelManager = new PersonnelManager();
        private PermissionTipManager permissionTipManager = new PermissionTipManager();
        private PermissionStateManager permissionStateManager = new PermissionStateManager();
        private UserManager userManager = new UserManager();

        public ActionResult Index()
        {
            ViewBag.TotalPersonnelCount = CacheHelper.GetPersonnelsFromCache().Count();
            return View(CacheHelper.GetPersonnelsFromCache());

        }

        public ActionResult ExitGivenPersonnelList()
        {
            ViewBag.ExitGivenPersonnelCount = personnelManager.ListQueryable()
                                             .Include("PersonnelDegrees").Include("PersonnelPositions").Include("PersonnelTasks")
                                             .Where(x => x.ExitOfJobDatetime != null)
                                             .OrderByDescending(x => x.CreatedOnDatetime).ToList().Count();

            return View("Index",personnelManager.ListQueryable()
                                             .Include("PersonnelDegrees").Include("PersonnelPositions").Include("PersonnelTasks")
                                             .Where(x=>x.ExitOfJobDatetime != null)
                                             .OrderByDescending(x => x.CreatedOnDatetime).ToList());
        }

        public ActionResult ActivePersonnelList()
        {
            ViewBag.ActivePersonnelCount = CacheHelper.GetActivePersonnelsFromCache();
            return View("Index", personnelManager.ListQueryable()
                                             .Include("PersonnelDegrees").Include("PersonnelPositions").Include("PersonnelTasks")
                                             .Where(x => x.ExitOfJobDatetime == null)
                                             .OrderByDescending(x => x.CreatedOnDatetime).ToList());
        }

        [HttpGet]
        public ActionResult Create()
        {
            TaskDegreePositionList();

            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(Personnels personnels)
        {
            ModelState.Remove("CreatedOnDatetime");
            ModelState.Remove("ModifiedOnDatetime");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                BusinessLayerResult<Personnels> res = personnelManager.InsertPersonnel(personnels);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    TaskDegreePositionListCreatOrEdit(personnels);
                    return View(personnels);
                }

                CacheHelper.RemoveGetPersonnelsFromCache();
                CacheHelper.RemoveGetActivePersonnelsFromCache();
                return RedirectToAction("Index");

            }
            TaskDegreePositionListCreatOrEdit(personnels);
            return View(personnels);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personnels personnels = personnelManager.Find(x => x.Id == id.Value);
            if (personnels == null)
            {
                return HttpNotFound();
            }
            TaskDegreePositionListCreatOrEdit(personnels);
            return View(personnels);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(Personnels personnel, HttpPostedFileBase ProfileImage)
        {
            ModelState.Remove("ModifiedOnDatetime");
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                if (ProfileImage != null &&
                      (ProfileImage.ContentType == "image/jpeg" ||
                       ProfileImage.ContentType == "image/jpg" ||
                       ProfileImage.ContentType == "image/png"))
                {
                    string filename = $"user_{personnel.Tc}.{ProfileImage.ContentType.Split('/')[1]}";
                    ProfileImage.SaveAs(Server.MapPath($"~/Images/{filename}"));
                    personnel.ProfileImage = filename;
                }

                BusinessLayerResult<Personnels> res = personnelManager.UpdatePersonnel(personnel);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                    TaskDegreePositionListCreatOrEdit(personnel);
                    return View(personnel);
                }

                CacheHelper.RemoveGetPersonnelsFromCache();
                CacheHelper.RemoveGetActivePersonnelsFromCache();
                return RedirectToAction("Index");

            }
            TaskDegreePositionListCreatOrEdit(personnel);
            return View(personnel);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personnels personnels = personnelManager.Find(x => x.Id == id.Value);
            if (personnels == null)
            {
                return HttpNotFound();
            }
            return View(personnels);
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            Personnels personnels = personnelManager.Find(x => x.Id == id.Value);
            if (personnels != null)
            {
                int res = personnelManager.Delete(personnels);
                if (res > 0)
                {
                    System.IO.File.Delete(Server.MapPath($"~/Images/{personnels.ProfileImage}"));
                    CacheHelper.RemoveGetPersonnelsFromCache();
                    CacheHelper.RemoveGetActivePersonnelsFromCache();
                    return Json(new { hasError = false, Message = $"{ personnels.Name + ' ' + personnels.Surname}" + " adlı personel başarılı bir şekilde silindi." }, JsonRequestBehavior.AllowGet);
                    //return Json(new { hasError = false, Massage = "Personel başarılı bir şekilde silindi." });
                }
                else
                {
                    return Json(new { hasError = true, Message = $"{ personnels.Name + ' ' + personnels.Surname}" + " adlı personel silinirken hata oluştu." }, JsonRequestBehavior.AllowGet);
                    //return Json(new { hasError = true, Message = "Personel silinirken hata oluştu." });
                }
            }
            return Json(new { results = true, Message = "Böyle bir personel bulunamadı." }, JsonRequestBehavior.AllowGet);
        }

        private void TaskDegreePositionList()
        {
            ViewBag.PersonnelDegreeList = new SelectList(CacheHelper.GetPersonnelDegreesFromCache(), "Id", "Name");
            ViewBag.PersonnelTaskList = new SelectList(CacheHelper.GetPersonnelTasksFromCache(), "Id", "Name");
            ViewBag.PersonnelPositionList = new SelectList(CacheHelper.GetPersonnelPositionsFromCache(), "Id", "Name");
        }

        private void TaskDegreePositionListCreatOrEdit(Personnels personnel)
        {
            ViewBag.PersonnelDegreeList = new SelectList(CacheHelper.GetPersonnelDegreesFromCache(), "Id", "Name", personnel.PersonnelDegreesId);
            ViewBag.PersonnelTaskList = new SelectList(CacheHelper.GetPersonnelTasksFromCache(), "Id", "Name", personnel.PersonnelTasksId);
            ViewBag.PersonnelPositionList = new SelectList(CacheHelper.GetPersonnelPositionsFromCache(), "Id", "Name", personnel.PersonnelPositionsId);
        }

        public ActionResult PersonnelPermissionList()
        {
            var result = personnelPermissionRequestManager.ListQueryable()
                                                             .Include("Personnels")
                                                             .Include("PersonnelPermissionTips")
                                                             .Include("PermissionStates")
                                                             .Include("Users")
                                                             .OrderByDescending(x => x.CreatedOnDatetime);
            //List<Personnels> personnelList = (from ppr in result
            //                                  select ppr).ToList();
            return View(result.ToList());
        }

        public ActionResult PersonnelIsApprovePermissionList()
        {
            var result = personnelPermissionRequestManager.ListQueryable()
                                                             .Include("Personnels")
                                                             .Include("PersonnelPermissionTips")
                                                             .Include("PermissionStates")
                                                             .Include("Users")
                                                             .Where(x => x.PermissionEndDatetime >= DateTime.Today && x.PermissionStatesId == 1)
                                                             .OrderByDescending(x => x.CreatedOnDatetime);

            return View("PersonnelPermissionList",result.ToList());
        }

        public ActionResult PersonnelPermissionOrReportedFinishedList()
        {
            var d = DateTime.Today.AddDays(-1);
            var result = personnelPermissionRequestManager.ListQueryable()
                                                             .Include("Personnels")
                                                             .Include("PersonnelPermissionTips")
                                                             .Include("PermissionStates")
                                                             .Include("Users")
                                                             .Where(x => x.PermissionEndDatetime == d && x.PermissionStatesId == 2)
                                                             .OrderByDescending(x => x.CreatedOnDatetime).Take(5);
            return View("PersonnelPermissionList", result.ToList());
        }

        public ActionResult PersonnelPermissionOrReportLastDayList()
        {
            var result = personnelPermissionRequestManager.ListQueryable()
                                                             .Include("Personnels")
                                                             .Include("PersonnelPermissionTips")
                                                             .Include("PermissionStates")
                                                             .Include("Users")
                                                             .Where(x => x.PermissionEndDatetime == DateTime.Today && x.PermissionStatesId == 2)
                                                             .OrderByDescending(x => x.CreatedOnDatetime).Take(5);
            return View("PersonnelPermissionList", result.ToList());
        }

        [HttpGet]
        public ActionResult PersonnelPermissionCreate(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Personnels per = personnelManager.Find(x => x.Id == id.Value);
            if (per == null)
            {
                return HttpNotFound();
            }

            ViewBag.PersonnelsId = per.Id;
            ViewBag.PersonnelNameSurname = per.Name + ' ' + per.Surname;
            ViewBag.PersonnelImage = per.ProfileImage;
            ViewBag.PermissionTip = new SelectList(permissionTipManager.List(), "Id", "Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult PersonnelPermissionCreate(PersonnelPermissionRequest personnelPermissionRequest)
        {
            ModelState.Remove("CreatedOnDatetime");
            ModelState.Remove("ModifiedOnDatetime");
            ModelState.Remove("ModifiedUsername");
            Personnels per = personnelManager.Find(x => x.Id == personnelPermissionRequest.PersonnelsId);
            if (ModelState.IsValid)
            {
                BusinessLayerResult<PersonnelPermissionRequest> res = personnelPermissionRequestManager.InsertPersonnelPermissionRequest(personnelPermissionRequest);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));

                    ViewBag.PersonnelsId = per.Id;
                    ViewBag.PersonnelNameSurname = per.Name + ' ' + per.Surname;
                    ViewBag.PersonnelImage = per.ProfileImage;
                    ViewBag.PermissionTip = new SelectList(permissionTipManager.List(), "Id", "Name");
                    return View(personnelPermissionRequest);
                }

                return RedirectToAction("PersonnelPermissionList");
            }

            ViewBag.PersonnelsId = per.Id;
            ViewBag.PersonnelNameSurname = per.Name + ' ' + per.Surname;
            ViewBag.PersonnelImage = per.ProfileImage;
            ViewBag.PermissionTip = new SelectList(permissionTipManager.List(), "Id", "Name");
            return View(personnelPermissionRequest);
        }

        [HttpGet]
        public ActionResult PersonnelPermissionEdit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonnelPermissionRequest personnelPermissionRequest = personnelPermissionRequestManager.Find(x => x.Id == id.Value);
            if (personnelPermissionRequest == null)
            {
                return HttpNotFound();
            }

            ViewBag.PersonnelsId = personnelPermissionRequest.PersonnelsId;
            ViewBag.NumberOfDays = personnelPermissionRequest.NumberofDays;
            ViewBag.PersonnelNameSurname = personnelPermissionRequest.Personnels.Name + ' ' + personnelPermissionRequest.Personnels.Surname;
            ViewBag.PersonnelImage = personnelPermissionRequest.Personnels.ProfileImage;
            ViewBag.PermissionTip = new SelectList(permissionTipManager.List(), "Id", "Name", personnelPermissionRequest.PersonnelPermissionTipsId);
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult PersonnelPermissionEdit(PersonnelPermissionRequest personnelPermissionRequest)
        {
            ModelState.Remove("ModifiedOnDatetime");
            ModelState.Remove("ModifiedUsername");
            Personnels per = personnelManager.Find(x => x.Id == personnelPermissionRequest.PersonnelsId);
            if (ModelState.IsValid)
            {
                BusinessLayerResult<PersonnelPermissionRequest> res = personnelPermissionRequestManager.UpdatePersonnelPermissionRequest(personnelPermissionRequest);
                if (res.Errors.Count > 0)
                {
                    res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));

                    ViewBag.PersonnelsId = per.Id;
                    ViewBag.PersonnelNameSurname = per.Name + ' ' + per.Surname;
                    ViewBag.PersonnelImage = per.ProfileImage;
                    ViewBag.PermissionTip = new SelectList(permissionTipManager.List(), "Id", "Name");
                    return View(personnelPermissionRequest);
                }

                return RedirectToAction("PersonnelPermissionList");
            }

            ViewBag.PersonnelsId = per.Id;
            ViewBag.PersonnelNameSurname = per.Name + ' ' + per.Surname;
            ViewBag.PersonnelImage = per.ProfileImage;
            ViewBag.PermissionTip = new SelectList(permissionTipManager.List(), "Id", "Name");
            return View(personnelPermissionRequest);

        }

        [HttpGet]
        public ActionResult PersonnelPermissionDetails(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonnelPermissionRequest personnelPermissionRequest = personnelPermissionRequestManager.Find(x => x.Id == id.Value);
            if (personnelPermissionRequest == null)
            {
                return HttpNotFound();
            }
            return View(personnelPermissionRequest);
        }

        [HttpPost]
        public ActionResult PersonnelPermissionDelete(int? id)
        {
            PersonnelPermissionRequest personnelPermissionRequest = personnelPermissionRequestManager.Find(x => x.Id == id.Value);
            var name = personnelPermissionRequest.Personnels.Name + ' ' + personnelPermissionRequest.Personnels.Surname;
            if (personnelPermissionRequest != null)
            {
                int res = personnelPermissionRequestManager.Delete(personnelPermissionRequest);
                if (res > 0)
                {
                    return Json(new { hasError = false, Message = $"{ name }" + " adlı personele verilen izin başarılı bir şekilde silindi." }, JsonRequestBehavior.AllowGet);
                    //return Json(new { hasError = false, Massage = "Personel başarılı bir şekilde silindi." });
                }
                else
                {
                    return Json(new { hasError = true, Message = $"{ name }" + " adlı personele verilen izin silinirken hata oluştu." }, JsonRequestBehavior.AllowGet);
                    //return Json(new { hasError = true, Message = "Personel silinirken hata oluştu." });
                }
            }
            return Json(new { results = true, Message = $"{ name }" + " adlı personelin izni bulunamadı." }, JsonRequestBehavior.AllowGet);
        }

        [AuthAdminOrKoordinator]
        [HttpGet]
        public ActionResult PersonnelPermissionCancel(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonnelPermissionRequest personnelPermissionRequest = personnelPermissionRequestManager.Find(x => x.Id == id.Value);
            if (personnelPermissionRequest == null)
            {
                return HttpNotFound();
            }
            return View(personnelPermissionRequest);
        }

        [AuthAdminOrKoordinator]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult PersonnelPermissionCancel(PersonnelPermissionRequest personnelPermissionRequest)
        {
            //PersonnelPermissionRequest per = personnelPermissionRequestManager.Find(x => x.Id == personnelPermissionRequest.Id);
            personnelPermissionRequest.Users = CurrentSession.User;
            BusinessLayerResult<PersonnelPermissionRequest> res = personnelPermissionRequestManager.CancelPersonnelPermissionRequest(personnelPermissionRequest);

            if (res.Errors.Count > 0)
            {
                res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                return View(personnelPermissionRequest);
            }

            return RedirectToAction("PersonnelPermissionList");

        }

        [AuthAdminOrKoordinator]
        [HttpGet]
        public ActionResult PersonnelPermissionApprove(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonnelPermissionRequest personnelPermissionRequest = personnelPermissionRequestManager.Find(x => x.Id == id.Value);
            if (personnelPermissionRequest == null)
            {
                return HttpNotFound();
            }

            return View(personnelPermissionRequest);
        }

        [AuthAdminOrKoordinator]
        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult PersonnelPermissionApprove(PersonnelPermissionRequest personnelPermissionRequest)
        {
            //PersonnelPermissionRequest per = personnelPermissionRequestManager.Find(x => x.Id == personnelPermissionRequest.Id);
            personnelPermissionRequest.Users = CurrentSession.User;
            BusinessLayerResult<PersonnelPermissionRequest> res = personnelPermissionRequestManager.ApprovePersonnelPermissionRequest(personnelPermissionRequest);

            if (res.Errors.Count > 0)
            {
                res.Errors.ForEach(x => ModelState.AddModelError("", x.Message));
                return View(personnelPermissionRequest);
            }

            return RedirectToAction("PersonnelPermissionList");

        }

        [HttpGet]
        public ActionResult PersonnelPermissionDetailsModal(int id)
        {
            List<Personnels> res = personnelManager.GetPersonnelPermissionDetails(id);
            if (res.Count == 0)
            {
                return HttpNotFound();
            }

            return PartialView("_PartialPersonnelPermissionDetails",res);
        }
    }
}