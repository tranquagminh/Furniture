using Furniture.Models.EF;
using PagedList;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Furniture.Controllers
{
    public class NewsController : Controller
    {
        FurnitureEntities _db = new FurnitureEntities();
        // GET: News
        public ActionResult Index()
        {
            return View();
        }
        public ActionResult Detail(long id, string meta)
        {
            var v = from t in _db.News
                    where t.id == id && t.meta == meta
                    select t;
            return View(v.FirstOrDefault());
        }
    }
}