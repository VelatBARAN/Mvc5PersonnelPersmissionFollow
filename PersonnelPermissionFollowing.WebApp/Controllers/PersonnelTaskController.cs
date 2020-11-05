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
    public class PersonnelTaskController : Controller
    {
        PersonnelTaskManager personnelTaskManager = new PersonnelTaskManager();
        public ActionResult Index()
        {
            return View(CacheHelper.GetPersonnelTasksFromCache());
            //return View(personnelTaskManager.ListQueryable().OrderByDescending(m=>m.CreatedOnDatetime).ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonnelTasks personnelTasks = personnelTaskManager.Find(x => x.Id == id.Value);
            if (personnelTasks == null)
            {
                return HttpNotFound();
            }
            return View(personnelTasks);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonnelTasks personnelTasks)
        {
            ModelState.Remove("CreatedOnDatetime");
            ModelState.Remove("ModifiedOnDatetime");
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                personnelTaskManager.Insert(personnelTasks);
                CacheHelper.RemoveGetPersonnelTasksFromCache();
                return RedirectToAction("Index");
            }

            CacheHelper.RemoveGetPersonnelTasksFromCache();
            return View(personnelTasks);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonnelTasks personnelTasks = personnelTaskManager.Find(x => x.Id == id.Value);
            if (personnelTasks == null)
            {
                return HttpNotFound();
            }
            return View(personnelTasks);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PersonnelTasks personnelTasks)
        {
            ModelState.Remove("CreatedOnDatetime");
            ModelState.Remove("ModifiedOnDatetime");
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                PersonnelTasks persTasks = personnelTaskManager.Find(x => x.Id == personnelTasks.Id);
                persTasks.Name = personnelTasks.Name;
                personnelTaskManager.Update(persTasks);
                CacheHelper.RemoveGetPersonnelTasksFromCache();
                return RedirectToAction("Index");
            }
            CacheHelper.RemoveGetPersonnelTasksFromCache();
            return View(personnelTasks);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonnelTasks personnelTasks = personnelTaskManager.Find(x => x.Id == id.Value);
            if (personnelTasks == null)
            {
                return HttpNotFound();
            }
            return View(personnelTasks);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonnelTasks personnelTasks = personnelTaskManager.Find(x => x.Id == id);
            personnelTaskManager.Delete(personnelTasks);
            CacheHelper.RemoveGetPersonnelTasksFromCache();
            return RedirectToAction("Index");
        }
    }
}
