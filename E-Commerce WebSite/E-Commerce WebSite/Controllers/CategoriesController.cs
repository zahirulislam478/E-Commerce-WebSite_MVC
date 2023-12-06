using E_Commerce_WebSite.Models;
using E_Commerce_WebSite.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using X.PagedList;
using X.PagedList.Mvc;
using X.PagedList.Mvc.Common;

namespace E_Commerce_WebSite.Controllers
{
    public class CategoriesController : Controller
    {
        private readonly EcommerceDbContext db = new EcommerceDbContext();
        // GET: Shop
        public ActionResult Index(int pg = 1)
        {
            var data = db.Categories.OrderBy(a => a.Id).ToPagedList(pg, 5);
            return View(data);
        }
        public ActionResult Create()
        {
            CategoryViewModel a = new CategoryViewModel();
            a.Products.Add(new Product { });
            return View(a);
        }
        [HttpPost]
        public ActionResult Create(CategoryViewModel data, string act = "")
        {
            if (act == "add")
            {
                data.Products.Add(new Product { });
                foreach (var item in ModelState.Values)
                {
                    item.Errors.Clear();
                }
            }
            if (act.StartsWith("remove"))
            {
                int index = int.Parse(act.Substring(act.IndexOf("_") + 1));
                data.Products.RemoveAt(index);
                foreach (var item in ModelState.Values)
                {
                    item.Errors.Clear();
                }
            }
            if (act == "insert")
            {
                if (ModelState.IsValid)
                {
                    var a = new Category
                    {
                        Name = data.Name,
                        Description = data.Description,
                        IsActive = data.IsActive,
                        Quantity = data.Quantity,
                        FeaturedDate = data.FeaturedDate,
                        AveragePrice = data.AveragePrice,
                        TopSellingProduct = data.TopSellingProduct
                    };
                    string ext = Path.GetExtension(data.Picture.FileName);
                    string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                    string savePath = Server.MapPath("~/Content/img/") + fileName;
                    data.Picture.SaveAs(savePath);
                    a.Picture = fileName;

                    foreach (var q in data.Products)
                    {
                        a.Products.Add(q);
                    }
                    db.Categories.Add(a);
                    db.SaveChanges();
                }
            }
            ViewBag.Act = act;
            return PartialView("_CreatePartial", data);
        }

        public ActionResult Edit(int id)
        {
            //explicit loading
            var x = db.Categories.FirstOrDefault(o => o.Id == id);
            db.Entry(x).Collection(o => o.Products).Load();
            var data = new CategoryEditModel
            {
                Id = x.Id,
                Name = x.Name,
                Description = x.Description,
                IsActive = x.IsActive,
                Quantity = x.Quantity,
                FeaturedDate = x.FeaturedDate,
                AveragePrice = x.AveragePrice,
                TopSellingProduct = x.TopSellingProduct,
                Products = x.Products.ToList()
            };
            ViewBag.CurrentPic = db.Categories.FirstOrDefault(o => o.Id == id).Picture;
            return View(data);

            //var a = db.Categories
            //  .Select(x => new CategoryEditModel
            //  {
            //      Id = x.Id,
            //      Name = x.Name,
            //      Description = x.Description,
            //      IsActive = x.IsActive,
            //      Quantity = x.Quantity,
            //      FeaturedDate = x.FeaturedDate,
            //      AveragePrice = x.AveragePrice,
            //      TopSellingProduct = x.TopSellingProduct,
            //      Products = x.Products.ToList()
            //  })
            //  .FirstOrDefault(x => x.Id == id);

            //ViewBag.CurrentPic = db.Categories.FirstOrDefault(x => x.Id == id).Picture;
            //return View(a);
        }

        [HttpPost]
        public ActionResult Edit(CategoryEditModel data, string act = "")
        {
            if (act == "add")
            {
                data.Products.Add(new Product { });
                foreach (var item in ModelState.Values)
                {
                    item.Errors.Clear();
                }
            }

            if (act.StartsWith("remove"))
            {
                int index = int.Parse(act.Substring(act.IndexOf("_") + 1));
                data.Products.RemoveAt(index);
                foreach (var item in ModelState.Values)
                {
                    item.Errors.Clear();
                }
            }

            if (act == "update")
            {
                if (ModelState.IsValid)
                {
                    var a = db.Categories.FirstOrDefault(x => x.Id == data.Id);
                    a.Name = data.Name;
                    a.Description = data.Description;
                    a.IsActive = data.IsActive;
                    a.Quantity = data.Quantity;
                    a.FeaturedDate = data.FeaturedDate;
                    a.AveragePrice = data.AveragePrice;
                    a.TopSellingProduct = data.TopSellingProduct;
                    if (data.Picture != null)
                    {
                        string ext = Path.GetExtension(data.Picture.FileName);
                        string fileName = Path.GetFileNameWithoutExtension(Path.GetRandomFileName()) + ext;
                        string savePath = Server.MapPath("~/Content/img/") + fileName;
                        data.Picture.SaveAs(savePath);
                        a.Picture = fileName;
                    }
                    db.Products.RemoveRange(db.Products.Where(x => x.CategoryId == data.Id).ToList());

                    foreach (var item in data.Products)
                    {
                        a.Products.Add(new Product
                        {
                            Id = data.Id,
                            Brand = item.Brand,
                            ProductName = item.ProductName,
                            Price = item.Price,
                            Rating = item.Rating
                        });
                    }
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }
            }
            ViewBag.CurrentPic = db.Categories.FirstOrDefault(x => x.Id == data.Id).Picture;
            return View(data);
        }
        [HttpPost]
        public ActionResult Delete(int id)
        {
            var category = new Category { Id = id };
            db.Entry(category).State = System.Data.Entity.EntityState.Deleted;
            db.SaveChanges();
            return Json(new { success = true, deleted = id });
        }
    }
}