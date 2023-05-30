using Furniture.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using PagedList;
namespace Furniture.Controllers
{
    public class DefaultController : Controller
    {
        FurnitureEntities nd = new FurnitureEntities();
        // GET: Default
        public ActionResult Index()
        {
            
            return View();
        }
        public ActionResult AboutUs()
        {

            return View();
        }
        public ActionResult getContact()
        {

            return View();
        }
        public ActionResult getMenu()
        {
            var v = from s in nd.Menus
                    where s.hide == true
                    orderby s.order ascending
                    select s;
            return PartialView(v.ToList());
        }
        public ActionResult getNews()
        {
            var v = from s in nd.News
                    where s.hide == true
                    orderby s.order ascending
                    select s;
            return PartialView(v.ToList());
        }
        public ActionResult getAllNews(int? page, int? pageSize, string searchString)
        {
            if (page == null)
            {
                page = 1;
            }
            if (pageSize == null)
            {
                pageSize = 6;
            }
            ViewData["CurrentFilter"] = searchString;

            var v = from s in nd.News
                    where s.hide == true
                    select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                v = v.Where(s => s.name.Contains(searchString) || s.description.Contains(searchString));
            }
            v = v.OrderBy(s => s.order);
            return PartialView(v.ToList().ToPagedList((int)page, (int)pageSize));
        }
        public ActionResult getProduct(long id, string metatitle,int? page, int? pageSize, string searchString) 
        {
            if (page == null)
            {
                page = 1;
            }
            if (pageSize == null)
            {
                pageSize = 3;
            }
            ViewData["CurrentFilter"] = searchString;
            ViewBag.meta = "san-pham";
            var v = from s in nd.Products
                    where s.categoryid == id && s.hide == true
                    select s;
            if (!String.IsNullOrEmpty(searchString))
            {
                v = v.Where(s => s.name.Contains(searchString));
            }
            v = v.OrderBy(s => s.order);
            return PartialView(v.ToList().ToPagedList((int)page, (int)pageSize));
        }
        public ActionResult getCategory()
        {
            ViewBag.meta = "san-pham";
            var v = from s in nd.categories
                    where s.hide == true
                    orderby s.order ascending
                    select s;
            return PartialView(v.ToList()); 
        }
        

    }
}