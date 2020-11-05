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
using PersonnelPermissionFollowing.WebApp.Models;

namespace PersonnelPermissionFollowing.WebApp.Controllers
{
    [Auth]
    public class PersonnelDegreeController : Controller
    {
        PersonnelDegreeManager personnelDegreeManager = new PersonnelDegreeManager();
        public ActionResult Index()
        {
            return View(CacheHelper.GetPersonnelDegreesFromCache());
            //return View(personnelDegreeManager.ListQueryable().OrderByDescending(x=>x.CreatedOnDatetime).ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonnelDegrees personnelDegrees = personnelDegreeManager.Find(x => x.Id == id.Value);
            if (personnelDegrees == null)
            {
                return HttpNotFound();
            }
            return View(personnelDegrees);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonnelDegrees personnelDegrees)
        {
            ModelState.Remove("CreatedOnDatetime");
            ModelState.Remove("ModifiedOnDatetime");
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                personnelDegreeManager.Insert(personnelDegrees);
                CacheHelper.RemoveGetPersonnelDegreesFromCache();
                return RedirectToAction("Index");
            }

            return View(personnelDegrees);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonnelDegrees personnelDegrees = personnelDegreeManager.Find(x => x.Id == id.Value);
            if (personnelDegrees == null)
            {
                return HttpNotFound();
            }
            return View(personnelDegrees);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PersonnelDegrees personnelDegrees)
        {
            ModelState.Remove("CreatedOnDatetime");
            ModelState.Remove("ModifiedOnDatetime");
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                PersonnelDegrees perDegrees = personnelDegreeManager.Find(x => x.Id == personnelDegrees.Id);
                perDegrees.Name = personnelDegrees.Name;
                personnelDegreeManager.Update(perDegrees);
                CacheHelper.RemoveGetPersonnelDegreesFromCache();
                return RedirectToAction("Index");
            }
            return View(personnelDegrees);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonnelDegrees personnelDegrees = personnelDegreeManager.Find(x => x.Id == id.Value);
            if (personnelDegrees == null)
            {
                return HttpNotFound();
            }
            return View(personnelDegrees);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonnelDegrees personnelDegrees = personnelDegreeManager.Find(x => x.Id == id);
            personnelDegreeManager.Delete(personnelDegrees);
            CacheHelper.RemoveGetPersonnelDegreesFromCache();
            return RedirectToAction("Index");
        }
    }
}
