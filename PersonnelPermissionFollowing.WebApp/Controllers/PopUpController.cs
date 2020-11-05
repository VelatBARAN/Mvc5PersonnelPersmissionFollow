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
    [AuthAdmin]
    public class PopUpController : Controller
    {
        PopUpManager popUpManager = new PopUpManager();

        public ActionResult Index()
        {
            return View(popUpManager.ListQueryable().OrderByDescending(x=>x.CreatedOnDatetime).ToList());
        }

        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PopUps popUps = popUpManager.Find(x => x.Id == id.Value);
            if (popUps == null)
            {
                return HttpNotFound();
            }
            return View(popUps);
        }

        public ActionResult Create()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create(PopUps popUps,HttpPostedFileBase PopUpImage)
        {
            ModelState.Remove("CreatedOnDatetime");
            ModelState.Remove("ModifiedOnDatetime");
            ModelState.Remove("ModifiedUsername");

            if (ModelState.IsValid)
            {
                if (PopUpImage != null &&
                      (PopUpImage.ContentType == "image/jpeg" ||
                       PopUpImage.ContentType == "image/jpg" ||
                       PopUpImage.ContentType == "image/png"))
                {
                    string filename = $"user_{popUps.Title}.{PopUpImage.ContentType.Split('/')[1]}";
                    PopUpImage.SaveAs(Server.MapPath($"~/Images/PopUpImage/{filename}"));
                    popUps.PopUpImage = filename;
                }

                popUpManager.Insert(popUps);
                return RedirectToAction("Index");
            }

            return View(popUps);
        }

        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PopUps popUps = popUpManager.Find(x => x.Id == id.Value);
            if (popUps == null)
            {
                return HttpNotFound();
            }
            return View(popUps);
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(PopUps popUps, HttpPostedFileBase PopUpImage)
        {
            ModelState.Remove("ModifiedOnDatetime");
            ModelState.Remove("ModifiedUsername");
            if (ModelState.IsValid)
            {
                PopUps popUpEdit = popUpManager.Find(x => x.Id == popUps.Id);
                if (PopUpImage != null &&
                      (PopUpImage.ContentType == "image/jpeg" ||
                       PopUpImage.ContentType == "image/jpg" ||
                       PopUpImage.ContentType == "image/png"))
                {
                    string filename = $"user_{popUps.Title}.{PopUpImage.ContentType.Split('/')[1]}";
                    PopUpImage.SaveAs(Server.MapPath($"~/Images/PopUpImage/{filename}"));
                    popUpEdit.PopUpImage = filename;
                }

                popUpEdit.Title = popUps.Title;
                popUpEdit.IsActive = popUps.IsActive;

                popUpManager.Update(popUpEdit);
                return RedirectToAction("Index");
            }
            return View(popUps);
        }

        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            PopUps popUps = popUpManager.Find(x => x.Id == id.Value);
            if (popUps == null)
            {
                return HttpNotFound();
            }
            return View(popUps);
        }

        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            PopUps popUps = popUpManager.Find(x => x.Id == id);
            popUpManager.Delete(popUps);
            System.IO.File.Delete(Server.MapPath($"~/Images/PopUpImage/{popUps.PopUpImage}"));
            return RedirectToAction("Index");
        }

    }
}
