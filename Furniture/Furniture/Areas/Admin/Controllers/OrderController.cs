using Furniture.Models;
using Furniture.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Furniture.Areas.Admin.Controllers
{
    public class OrderController : Controller
    {
        // GET: Admin/Order
        FurnitureEntities db = new FurnitureEntities();
        public ActionResult Index(string searchTxt)
        {
            IEnumerable<Order_tb> item = db.Order_tb;
            if (!string.IsNullOrEmpty(searchTxt))
            {
                item = item.Where(x => x.Addres.Contains(searchTxt) || x.Phone.Contains(searchTxt)
                                    || x.Code.Contains(searchTxt));
            }

            return View(item);
        }

        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.Order_tb.Find(id);
            var v = from s in db.OrderDetails
                    where s.OrderID == id
                    select s;
            db.OrderDetails.RemoveRange(v);
            db.SaveChanges();
            if (item != null)
            {

                db.Order_tb.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        public ActionResult Detail(int id)
        {
            var v = from s in db.OrderDetails
                    where s.OrderID == id
                    select s;

            return View(v.ToList());
        }
    }
}