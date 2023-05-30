using Furniture.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Furniture.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Employee")]
    public class CategoryController : Controller
    {
        // GET: Admin/Category
        FurnitureEntities db = new FurnitureEntities();
        public ActionResult Index()
        {
            var v = db.categories;
            return View(v);
        }
        public ActionResult Add()
        {
            return View();
        }

        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(category item)
        {
            if (ModelState.IsValid)
            {
                item.meta = Furniture.Models.Common.Filter.FilterChar(item.name);
                item.datebegin = DateTime.Now;
                db.categories.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }
        public ActionResult Edit(int id)
        {
            var model = db.categories.Find(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(category item)
        {
            if (ModelState.IsValid)
            {
                item.meta = Furniture.Models.Common.Filter.FilterChar(item.name);
                item.datebegin = DateTime.Now;
                db.categories.Attach(item);
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.categories.Find(id);
            if (item != null)
            {
                db.categories.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}