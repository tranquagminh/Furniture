using Furniture.Models.EF;
using Microsoft.Ajax.Utilities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Furniture.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Employee")]
    public class MenuController : Controller
    {
        FurnitureEntities db = new FurnitureEntities();
        // GET: Admin/Menu
        public ActionResult Index()
        {
            var item = db.Menus;
            return View(item);
        }
        public ActionResult Add()
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Menu model)
        {
            if (ModelState.IsValid)
                    {
                        model.databegin = DateTime.Now;
                        model.meta = Furniture.Models.Common.Filter.FilterChar(model.name);
                        model.hide = true;
                        db.Menus.Add(model);
                        db.SaveChanges();
                        return RedirectToAction("Index");
                    }
                
                
            return View(model);
        }
        public ActionResult Edit(int id)
        {
            var v = db.Menus.Find(id);
            return View(v);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Menu model)
        {
            if (ModelState.IsValid)
            {
                db.Menus.Attach(model);
                model.databegin = DateTime.Now;
                model.hide = true;
                model.meta = Furniture.Models.Common.Filter.FilterChar(model.name);
                db.Entry(model).Property(x => x.name).IsModified = true;
                db.Entry(model).Property(x => x.link).IsModified = true;
                db.Entry(model).Property(x => x.meta).IsModified = true;
                db.Entry(model).Property(x => x.hide).IsModified = true;
                db.Entry(model).Property(x => x.order).IsModified = true;
                db.Entry(model).Property(x => x.databegin).IsModified = true;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(model);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.Menus.Find(id);
            if(item != null)
            {
                db.Menus.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}