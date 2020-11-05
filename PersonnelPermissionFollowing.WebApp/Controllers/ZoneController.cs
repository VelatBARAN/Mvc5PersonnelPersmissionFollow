using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PersonnelPermissionFollowing.BusinessLayer;
using PersonnelPermissionFollowing.Entities;
using PersonnelPermissionFollowing.WebApp.Filters;

namespace PersonnelPermissionFollowing.WebApp.Controllers
{
    [Auth]
    public class ZoneController : Controller
    {
        private ZoneManager zoneManager = new ZoneManager();
        private PersonnelManager personnelsManager = new PersonnelManager();

        public ActionResult Index()
        {
            return View(zoneManager.ListQueryable()
                                                   .Include("Personnels")
                                                   .Where(m => m.Personnels.ExitOfJobDatetime == null)
                                                   .OrderByDescending(m => m.CreatedOnDatetime).ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zones zones = zoneManager.Find(m => m.Id == id.Value);
            if (zones == null)
            {
                return HttpNotFound();
            }
            return View(zones);
        }

        public ActionResult Create()
        {
            PersonelList();
            //ViewBag.PersonnelAmirSefList = new SelectList(personnelsManager.List()
            //                                                               .Where(x=>x.PersonnelTasks.Name.Contains("Amir") ||
            //                                                                      x.PersonnelTasks.Name.Contains("Şef")),"Id","Name");
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(Zones zones)
        {
            ModelState.Remove("CreatedOnDatetime");
            ModelState.Remove("ModifiedOnDatetime");
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                zoneManager.Insert(zones);
                return RedirectToAction("Index");
            }

            PersonelList();

            return View(zones);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zones zones = zoneManager.Find(m => m.Id == id.Value);
            if (zones == null)
            {
                return HttpNotFound();
            }
            PersonelList();

            return View(zones);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Zones zones)
        {
            ModelState.Remove("CreatedOnDatetime");
            ModelState.Remove("ModifiedOnDatetime");
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                Zones zone = zoneManager.Find(x => x.Id == zones.Id);
                zone.Name = zones.Name;
                zone.PersonnelsId = zones.PersonnelsId;
                zoneManager.Update(zone);
                return RedirectToAction("Index");
            }

            PersonelList();

            return View(zones);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Zones zones = zoneManager.Find(x => x.Id == id.Value);
            if (zones == null)
            {
                return HttpNotFound();
            }
            return View(zones);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Zones zones = zoneManager.Find(x => x.Id == id);
            zoneManager.Delete(zones);
            return RedirectToAction("Index");
        }

        private void PersonelList()
        {
            List<SelectListItem> personnelAmirSefList = (from k in personnelsManager.List(x => x.ExitOfJobDatetime == null)
                                                         where k.PersonnelTasks.Name.Contains("Amir") || k.PersonnelTasks.Name.Contains("Şef")
                                                         select new SelectListItem
                                                         {
                                                             Text = k.Name + " " + k.Surname,
                                                             Value = k.Id.ToString()
                                                         }).ToList();

            ViewBag.PersonnelAmirSefList = personnelAmirSefList;
        }
    }
}
