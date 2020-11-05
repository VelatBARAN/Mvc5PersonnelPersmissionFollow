using PersonnelPermissionFollowing.BusinessLayer;
using PersonnelPermissionFollowing.Entities;
using PersonnelPermissionFollowing.WebApp.Filters;
using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Security.Cryptography;
using System.Web;
using System.Web.Mvc;

namespace PersonnelPermissionFollowing.WebApp.Controllers
{
    [Auth]
    public class AssigningTaskOfPersonnelController : Controller
    {
        private AssigningTaskOfPersonnelManager assigningTaskOfPersonnelManager = new AssigningTaskOfPersonnelManager();
        private PersonnelManager personnelsManager = new PersonnelManager();
        private ZoneManager zoneManager = new ZoneManager();
        private WeekdaysManager weekdaysManager = new WeekdaysManager();
        public ActionResult Index()
        {
            var result = assigningTaskOfPersonnelManager.ListQueryable()
                                                       .Include("Personnels").Include("Zones").Include("Weekdays")
                                                       .Where(m => m.Personnels.ExitOfJobDatetime == null)
                                                       .OrderByDescending(m => m.ModifiedOnDatetime).ToList();
            return View(result);
        }

        [HttpGet]
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssigningTaskOfPersonnel assigningTaskOfPersonnel = assigningTaskOfPersonnelManager.Find(m => m.Id == id.Value);
            if (assigningTaskOfPersonnel == null)
            {
                return HttpNotFound();
            }

            return View(assigningTaskOfPersonnel);
        }

        [HttpGet]
        public ActionResult Create()
        {
            PersonnelList();
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Create(AssigningTaskOfPersonnel assigningTaskOfPersonnel)
        {
            ModelState.Remove("CreatedOnDatetime");
            ModelState.Remove("ModifiedOnDatetime");
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                assigningTaskOfPersonnelManager.Insert(assigningTaskOfPersonnel);
                return RedirectToAction("Index");
            }

            PersonnelZoneWeekCreateOrDeleteList(assigningTaskOfPersonnel);
            return View(assigningTaskOfPersonnel);
        }

        [HttpGet]
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            AssigningTaskOfPersonnel assigedTaskOfPersonnel = assigningTaskOfPersonnelManager.Find(x => x.Id == id.Value);
            if (assigedTaskOfPersonnel == null)
            {
                return HttpNotFound();
            }
            PersonnelZoneWeekCreateOrDeleteList(assigedTaskOfPersonnel);
            return View(assigedTaskOfPersonnel);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        [ValidateInput(false)]
        public ActionResult Edit(AssigningTaskOfPersonnel assigningTaskOfPersonnel)
        {
            ModelState.Remove("ModifiedOnDatetime");
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                AssigningTaskOfPersonnel assigedTaskOfPersonnel = assigningTaskOfPersonnelManager.Find(x => x.Id == assigningTaskOfPersonnel.Id);
                assigedTaskOfPersonnel.ZonesId = assigningTaskOfPersonnel.ZonesId;
                assigedTaskOfPersonnel.PersonnelsId = assigningTaskOfPersonnel.PersonnelsId;
                assigedTaskOfPersonnel.WeekdaysId = assigningTaskOfPersonnel.WeekdaysId;
                assigedTaskOfPersonnel.TaskDescription = assigningTaskOfPersonnel.TaskDescription;
                assigedTaskOfPersonnel.Phone = assigningTaskOfPersonnel.Phone;
                assigningTaskOfPersonnelManager.Update(assigedTaskOfPersonnel);
                return RedirectToAction("Index");
            }

            PersonnelZoneWeekCreateOrDeleteList(assigningTaskOfPersonnel);
            return View(assigningTaskOfPersonnel);
        }

        [HttpPost]
        public ActionResult Delete(int? id)
        {
            AssigningTaskOfPersonnel assigningTaskOfPersonnel = assigningTaskOfPersonnelManager.Find(x => x.Id == id.Value);
            var name = assigningTaskOfPersonnel.Personnels.Name + ' ' + assigningTaskOfPersonnel.Personnels.Surname;
            if (assigningTaskOfPersonnel != null)
            {
                int res = assigningTaskOfPersonnelManager.Delete(assigningTaskOfPersonnel);
                if (res > 0)
                {
                    return Json(new { hasError = false, Message = $"{ name }" + " adlı personelin görevi başarılı bir şekilde silindi." }, JsonRequestBehavior.AllowGet);
                    //return Json(new { hasError = false, Massage = "Personel başarılı bir şekilde silindi." });
                }
                else
                {
                    return Json(new { hasError = true, Message = $"{ name }" + " adlı personelin görevi silinirken hata oluştu." }, JsonRequestBehavior.AllowGet);
                    //return Json(new { hasError = true, Message = "Personel silinirken hata oluştu." });
                }
            }
            return Json(new { results = true, Message = $"{ name }" + " adlı bir personel bulunamadı." }, JsonRequestBehavior.AllowGet);
        }

        private void PersonnelList()
        {
            //ViewBag.PersonnelList = new SelectList(personnelsManager.List(), "Id", "Name", "Surname");
            var zonePersonelId = zoneManager.List().Select(x => x.Personnels.Id);
            var personnelAssignedTask = personnelsManager.List(x => x.IsAssignedTask == false);

            List<SelectListItem> personnelList = (from k in personnelAssignedTask
                                                  where !zonePersonelId.Contains(k.Id) && k.ExitOfJobDatetime == null
                                                  select new SelectListItem
                                                  {
                                                      Text = k.Tc + " " + k.Name + " " + k.Surname,
                                                      Value = k.Id.ToString()
                                                  }).ToList();

            ViewBag.PersonnelList = personnelList;
            ViewBag.ZoneList = new SelectList(zoneManager.List(), "Id", "Name");
            ViewBag.TaskVacationList = new SelectList(weekdaysManager.List(), "Id", "Name");
        }

        private void PersonnelZoneWeekCreateOrDeleteList(AssigningTaskOfPersonnel assigningTaskOfPersonnel)
        {
            //ViewBag.PersonnelList = new SelectList(personnelsManager.List(), "Id", "Name", "Surname");
            var zonePersonelId = zoneManager.List().Select(x => x.Personnels.Id);
            var personnelAssignedTask = personnelsManager.List(x => x.IsAssignedTask == false || x.Id == assigningTaskOfPersonnel.PersonnelsId);

            List<SelectListItem> personnelList = (from k in personnelAssignedTask
                                                  where !zonePersonelId.Contains(k.Id) &&  k.ExitOfJobDatetime == null 
                                                  select new SelectListItem
                                                  {
                                                      Text = k.Name + " " + k.Surname,
                                                      Value = k.Id.ToString()
                                                  }).ToList();

            ViewBag.PersonnelList = personnelList;
            ViewBag.ZoneList = new SelectList(zoneManager.List(), "Id", "Name", assigningTaskOfPersonnel.ZonesId);
            ViewBag.TaskVacationList = new SelectList(weekdaysManager.List(), "Id", "Name", assigningTaskOfPersonnel.WeekdaysId);
        }


    }
}