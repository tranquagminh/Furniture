using Furniture.Models.EF;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace Furniture.Areas.Admin.Controllers
{
    [Authorize(Roles = "Admin, Employee")]
    public class ProductController : Controller
    {
        // GET: Admin/Product
        FurnitureEntities db = new FurnitureEntities();
        public ActionResult Index(long ? id = null)
        {
            getCategory(id);
            return View();
        }

        
        public void getCategory(long? selectedId = null)
        {
            ViewBag.Category = new SelectList(db.categories.Where(x => x.hide == true)
                .OrderBy(x => x.order), "id", "name", selectedId);
        }
        public ActionResult getProduct(long? id)
        {
            if (id == null)
            {
                var v = db.Products.OrderBy(x => x.order).ToList();
                return PartialView(v);
            }
            var m = db.Products.Where(x => x.categoryid == id).OrderBy(x => x.order).ToList();
            return PartialView(m);
        }
        public ActionResult Add() 
        {
            ViewBag.ProductCategory = new SelectList(db.categories.ToList(),"id","name");
            return View();
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Add(Product product,List<string> Images,List<int> ImgAvatar)
        {
            if (ModelState.IsValid)
            {
                if(Images != null && Images.Count > 0)
                {
                    for(int i=0;i<Images.Count;i++)
                    {
                        if(i+1 == ImgAvatar[0])
                        {
                            product.img = Images[i];
                            db.ProductImages.Add(new ProductImage
                            {
                                productID = product.id,
                                img = Images[i],
                                Avatar = true
                            }) ;
                        }
                        else
                        {
                            db.ProductImages.Add(new ProductImage
                            {
                                productID = product.id,
                                img = Images[i],
                                Avatar = false
                            }) ;
                        }
                        
                    }
                }
                product.meta =  db.categories.Find(product.categoryid).meta;
                product.datebegin= DateTime.Now;
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ProductCategory = new SelectList(db.categories.ToList(), "id", "name");
            return View(product);
        }
        public ActionResult Edit(int id)
        {
            ViewBag.ProductCategory = new SelectList(db.categories.ToList(), "id", "name");
            var v = db.Products.Find(id);
            return View(v);
        }
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit(Product item, List<string> Images, List<int> ImgAvatar,List<string> OldImages)
        {
            ViewBag.ProductCategory = new SelectList(db.categories.ToList(), "id", "name");
            if (ModelState.IsValid)
            {
                if(OldImages != null && OldImages.Count > 0 )
                {
                    int temp = ImgAvatar[0];
                       
                    var u = db.ProductImages.Where(s => s.id == temp);
                        foreach (var image in u)
                        {
                            
                            image.Avatar = true;
                            db.ProductImages.Attach(image);
                            db.Entry(image).State = System.Data.Entity.EntityState.Modified;
                           
                        }
                    db.SaveChanges();
                    var k = db.ProductImages.Where(s => s.Avatar == true && s.productID == item.id);
                    if(k.Count() >=2)
                    {
                        foreach(var imag in k)
                        {
                            if(imag.id != temp)
                            {
                                imag.Avatar = false;
                                db.ProductImages.Attach(imag);
                                db.Entry(imag).State = System.Data.Entity.EntityState.Modified;
                                
                            }
                            else
                            {
                                item.img = imag.img;
                            }

                        }
                        db.SaveChanges();
                    }
                    else
                    {
                        item.img = k.FirstOrDefault(s=>s.Avatar == true).img;
                    }
                }
                if (Images != null && Images.Count > 0)
                {
                    for (int i = 0; i < Images.Count; i++)
                    {
                        if (i + 1 == ImgAvatar[0])
                        {
                         
                            var x = db.ProductImages.Where(a => a.Avatar == true && a.id == item.id).ToList();
                            foreach (var image in x)
                            {
                                image.id = image.id;
                                image.productID = image.productID;
                                image.img = image.img;
                                image.Avatar = false;
                                db.ProductImages.Attach(image);
                                db.Entry(image).State = System.Data.Entity.EntityState.Modified;
                               
                            }
                            db.SaveChanges();
                            item.img = Images[i];
                            db.ProductImages.Add(new ProductImage
                            {
                                productID = item.id,
                                img = Images[i],
                                Avatar = true
                            });
                        }
                        else
                        {
                            db.ProductImages.Add(new ProductImage
                            {
                                productID = item.id,
                                img = Images[i],
                                Avatar = false
                            });
                        }

                    }
                }
                item.meta = db.categories.Find(item.categoryid).meta;
                item.datebegin = DateTime.Now;
                db.Products.Attach(item);
                db.Entry(item).State = System.Data.Entity.EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(item);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var item = db.Products.Find(id);
            
            if (item != null)
            {
                var x = db.ProductImages.Where(a => a.productID == item.id);
                if (x != null) {
                    db.ProductImages.RemoveRange(x);
                    db.SaveChanges();
                }
                db.Products.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }
        [HttpPost]
        public ActionResult DeleteImg(int id)
        {
            var item = db.ProductImages.Find(id);

            if (item != null)
            {
                
                db.ProductImages.Remove(item);
                db.SaveChanges();
                return Json(new { success = true });
            }
            return Json(new { success = false });
        }

    }
}