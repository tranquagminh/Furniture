using Furniture.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Furniture.Controllers
{
    public class ProductController : Controller
    {
        FurnitureEntities _db = new FurnitureEntities();
        // GET: Product
        public ActionResult Index(string meta)
        {
            var v = from t in _db.categories
                    where t.meta == meta
                    select t;
            return View(v.FirstOrDefault());
        }
        public ActionResult Detail(long id)
        {
            var v = from t in _db.Products
                    where t.id == id
                    select t;
            return View(v.FirstOrDefault());
        }
    }
}