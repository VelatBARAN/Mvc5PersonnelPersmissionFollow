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
    public class PersonnelPermissionTipController : Controller
    {
        PersonnelPermissionTipManager personnelPermissionTipManager = new PersonnelPermissionTipManager();
        public ActionResult Index()
        {
            return View(personnelPermissionTipManager.ListQueryable().OrderByDescending(x=>x.CreatedOnDatetime));
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonnelPermissionTips personnelPermissionTips = personnelPermissionTipManager.Find(x => x.Id == id.Value);
            if (personnelPermissionTips == null)
            {
                return HttpNotFound();
            }
            return View(personnelPermissionTips);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PersonnelPermissionTips personnelPermissionTips)
        {
            ModelState.Remove("CreatedOnDatetime");
            ModelState.Remove("ModifiedOnDatetime");
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                personnelPermissionTipManager.Insert(personnelPermissionTips);
                return RedirectToAction("Index");
            }

            return View(personnelPermissionTips);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonnelPermissionTips personnelPermissionTips = personnelPermissionTipManager.Find(x => x.Id == id.Value);
            if (personnelPermissionTips == null)
            {
                return HttpNotFound();
            }
            return View(personnelPermissionTips);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PersonnelPermissionTips personnelPermissionTips)
        {
            ModelState.Remove("CreatedOnDatetime");
            ModelState.Remove("ModifiedOnDatetime");
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                PersonnelPermissionTips perPermissionTips = personnelPermissionTipManager.Find(x => x.Id == personnelPermissionTips.Id);
                perPermissionTips.Name = personnelPermissionTips.Name;
                personnelPermissionTipManager.Update(perPermissionTips);

                return RedirectToAction("Index");
            }
            return View(personnelPermissionTips);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PersonnelPermissionTips personnelPermissionTips = personnelPermissionTipManager.Find(x => x.Id == id.Value);
            if (personnelPermissionTips == null)
            {
                return HttpNotFound();
            }
            return View(personnelPermissionTips);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PersonnelPermissionTips personnelPermissionTips = personnelPermissionTipManager.Find(x => x.Id == id);
            personnelPermissionTipManager.Delete(personnelPermissionTips);

            return RedirectToAction("Index");
        }

    }
}
