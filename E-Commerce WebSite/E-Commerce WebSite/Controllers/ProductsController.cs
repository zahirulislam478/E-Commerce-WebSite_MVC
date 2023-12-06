using E_Commerce_WebSite.Models;
using E_Commerce_WebSite.ViewModels;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Numerics;
using System.Threading.Tasks;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using X.PagedList;
namespace E_Commerce_WebSite.Controllers
{
    public class ProductsController : Controller
    {
        private readonly EcommerceDbContext db = new EcommerceDbContext();
        public ActionResult Index(int? page)
        {
            int pageSize = 4; // Number of products per page
            int pageNumber = (page ?? 1);

            var allProducts = db.Products.OrderBy(p => p.Id); // Modify your query as needed

            var pagedProducts = allProducts.ToPagedList(pageNumber, pageSize);

            return View(pagedProducts);
        }

    }
}