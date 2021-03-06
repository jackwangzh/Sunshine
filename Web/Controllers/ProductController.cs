﻿using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using Sunshine.Business.Core;
using Sunshine.Filters;

namespace Sunshine.Controllers
{
    [Authorize]
    [InitializeSimpleMembership]
    public class ProductController : Controller
    {
        private UsersContext db = new UsersContext();

        public ProductController()
            : base()
        {
            ViewBag.ModuleName = "产品管理";
        }
        //
        // GET: /Product/

        public ActionResult Index()
        {
            var product = db.Products.ToDictionary<Product, long>((a) => { return a.ProductId; });
            product.Add(0, new Product() { ProductName = "无" });
            ViewData["Product"] = product;
            return View(db.Products.ToList());
        }

        [HttpGet]
        [AllowAnonymous]
        public ActionResult Search(string pattern, string type)
        {
            //var aaa = db.Products.Where(a => a.ProductMark.Contains(pattern));
            //var c = aaa.ToString();
            ViewBag.keyword = pattern;
            if (type == "1")
                return View(db.Products.Where(a => a.ProductName.Contains(pattern)).ToList());
            else
                return View(db.Products.Where(a => a.ProductMark.Contains(pattern)).ToList());
        }
        //
        // GET: /Product/Details/5


        public ActionResult Details(long id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }
        [AllowAnonymous]
        public ActionResult Display(long id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // GET: /Product/Create

        public ActionResult Create()
        {
            ProductManager productmanager = new ProductManager();
            IList<Category> CategoryList = productmanager.getCategory(0);
            ViewData["categorylist"] = (from s in CategoryList
                                        select new SelectListItem
                                        {
                                            Selected = (s.CategoryId == 0),
                                            Text = s.CategoryName,
                                            Value = s.CategoryId.ToString()
                                        }).ToList();

            IList<Category> SecondCategoryList = productmanager.getCategory(1);
            ViewData["secondcategorylist"] = (from s in SecondCategoryList
                                              select new SelectListItem
                                              {
                                                  Selected = (s.CategoryId == 0),
                                                  Text = s.CategoryName,
                                                  Value = s.CategoryId.ToString()
                                              }).ToList();

            IList<Brand> BrandList = productmanager.getBrand();
            ViewData["brandlist"] = (from s in BrandList
                                     select new SelectListItem
                                     {
                                         Selected = (s.BrandId == 0),
                                         Text = s.BrandName,
                                         Value = s.BrandId.ToString()
                                     }).ToList();

            IList<PriceInterval> ProductIntervalList = productmanager.getPriceInterval();
            ViewData["productintervallist"] = (from s in ProductIntervalList
                                               select new SelectListItem
                                               {
                                                   Selected = (s.PriceIntervalId == 0),
                                                   Text = s.PriceIntervalName,
                                                   Value = s.PriceIntervalId.ToString()
                                               }).ToList();

            IList<ProductSize> ProductSizeList = productmanager.getProductSize();
            ViewData["productsizelist"] = (from s in ProductSizeList
                                           select new SelectListItem
                                           {
                                               Selected = (s.ProductSizeId == 0),
                                               Text = s.ProductSizeName,
                                               Value = s.ProductSizeId.ToString()
                                           }).ToList();

            return View();
        }

        public JsonResult ExtraProperty(int? productTypeId)
        {
            //var Properties = ProductManager.GetPropertiesForProductType(productTypeId);
            return Json("");//Properties);
        }

        //
        // POST: /Product/Create

        [HttpPost]
        public ActionResult Create(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Products.Add(product);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(product);
        }

        //
        // GET: /Product/Edit/5

        public ActionResult Edit(long id = 0)
        {
            Product product = db.Products.Find(id);

            #region
            ProductManager productmanager = new ProductManager();
            IList<Category> CategoryList = productmanager.getCategory(0);
            ViewData["categorylist"] = (from s in CategoryList
                                        select new SelectListItem
                                        {
                                            Selected = (s.CategoryId == product.CategoryId),
                                            Text = s.CategoryName,
                                            Value = s.CategoryId.ToString()
                                        }).ToList();

            IList<Category> SecondCategoryList = productmanager.getCategory(1);
            ViewData["secondcategorylist"] = (from s in SecondCategoryList
                                              select new SelectListItem
                                              {
                                                  Selected = (s.CategoryId == product.SecondCategoryId),
                                                  Text = s.CategoryName,
                                                  Value = s.CategoryId.ToString()
                                              }).ToList();

            IList<Brand> BrandList = productmanager.getBrand();
            ViewData["brandlist"] = (from s in BrandList
                                     select new SelectListItem
                                     {
                                         Selected = (s.BrandId == product.BrandId),
                                         Text = s.BrandName,
                                         Value = s.BrandId.ToString()
                                     }).ToList();

            IList<PriceInterval> ProductIntervalList = productmanager.getPriceInterval();
            ViewData["productintervallist"] = (from s in ProductIntervalList
                                               select new SelectListItem
                                               {
                                                   Selected = (s.PriceIntervalId == product.PriceIntervalId),
                                                   Text = s.PriceIntervalName,
                                                   Value = s.PriceIntervalId.ToString()
                                               }).ToList();

            IList<ProductSize> ProductSizeList = productmanager.getProductSize();
            ViewData["productsizelist"] = (from s in ProductSizeList
                                           select new SelectListItem
                                           {
                                               Selected = (s.ProductSizeId == product.ProductSizeId),
                                               Text = s.ProductSizeName,
                                               Value = s.ProductSizeId.ToString()
                                           }).ToList();
            #endregion


            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /Product/Edit/5

        [HttpPost]
        public ActionResult Edit(Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(product);
        }

        //
        // GET: /Product/Delete/5

        public ActionResult Delete(long id = 0)
        {
            Product product = db.Products.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        //
        // POST: /Product/Delete/5

        [HttpPost, ActionName("Delete")]
        public ActionResult DeleteConfirmed(long id)
        {
            Product product = db.Products.Find(id);
            db.Products.Remove(product);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            db.Dispose();
            base.Dispose(disposing);
        }
    }
}