using Furniture.Models.EF;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Furniture.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Employee")]
    public class NewsController : Controller
    {
        // GET: Admin/News
        FurnitureEntities db = new FurnitureEntities();
        public ActionResult Index(string searchTxt)
        {
            IEnumerable<News> item = db.News;
            if (!string.IsNullOrEmpty(searchTxt))
            {
                item = item.Where(x => x.name.Contains(searchTxt) || x.meta.Contains(searchTxt));
            }
            //var v = db.News;
            return View(item);
        }
        public ActionResult Add() 
        {
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(News item) 
        {
           

            if(ModelState.IsValid)
            {

                item.meta = Furniture.Models.Common.Filter.FilterChar(item.name);
                item.datebegin = DateTime.Now;
                db.News.Add(item);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }

        public ActionResult Edit(int id)
        {
            var model = db.News.Find(id);
            return View(model);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(News item)
        {
            if (ModelState.IsValid)
            {
                item.meta = Furniture.Models.Common.Filter.FilterChar(item.name);
                item.datebegin = DateTime.Now;
                db.News.Attach(item);
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.News.Find(id);
            if(item!=null )
            {
                db.News.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
    }
}