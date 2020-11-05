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
    public class PersonnelPositionController : Controller
    {
        PersonnelPositionManager personnelPositionManager = new PersonnelPositionManager();
        public ActionResult Index()
        {
            return View(CacheHelper.GetPersonnelPositionsFromCache());
            //return View(personnelPositionManager.ListQueryable().OrderByDescending(m=>m.CreatedOnDatetime).ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonnelPositions personnelPositions = personnelPositionManager.Find(x => x.Id == id.Value);
            if (personnelPositions == null)
            {
                return HttpNotFound();
            }
            return View(personnelPositions);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonnelPositions personnelPositions)
        {
            ModelState.Remove("CreatedOnDatetime");
            ModelState.Remove("ModifiedOnDatetime");
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                personnelPositionManager.Insert(personnelPositions);
                CacheHelper.RemoveGetPersonnelPositionsFromCache();
                return RedirectToAction("Index");
            }

            return View(personnelPositions);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonnelPositions personnelPositions = personnelPositionManager.Find(x => x.Id == id.Value);
            if (personnelPositions == null)
            {
                return HttpNotFound();
            }
            return View(personnelPositions);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PersonnelPositions personnelPositions)
        {
            ModelState.Remove("CreatedOnDatetime");
            ModelState.Remove("ModifiedOnDatetime");
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                PersonnelPositions perPosition = personnelPositionManager.Find(x => x.Id == personnelPositions.Id);
                perPosition.Name = personnelPositions.Name;
                personnelPositionManager.Update(personnelPositions);
                CacheHelper.RemoveGetPersonnelPositionsFromCache();
                return RedirectToAction("Index");
            }
            return View(personnelPositions);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonnelPositions personnelPositions = personnelPositionManager.Find(x => x.Id == id.Value);
            if (personnelPositions == null)
            {
                return HttpNotFound();
            }
            return View(personnelPositions);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonnelPositions personnelPositions = personnelPositionManager.Find(x => x.Id == id);
            personnelPositionManager.Delete(personnelPositions);
            CacheHelper.RemoveGetPersonnelPositionsFromCache();
            return RedirectToAction("Index");
        }
    }
}
