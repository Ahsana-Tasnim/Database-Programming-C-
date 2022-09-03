using DBProgramming_II.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBProgramming_II.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult ProductDetails(string id)
        {
            var context = new BooksEntities();
            Product product = context.Products.FirstOrDefault(x => x.ProductCode == id);

            return PartialView(product);
        }

        public ActionResult Index()
        {
            var context = new BooksEntities();
            List<Product> products = context.Products.OrderByDescending(x => x.InvoiceLineItems.Count).ToList();

            ViewBag.mySubTitle = products.Count.ToString() + " Books Available"; //dynamic

            AllDataLists allDataLists = new AllDataLists()
            {
                Products = products
            };

            return View(allDataLists);
        }


        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            var context = new BooksEntities();

            try
            {
                context.Products.AddOrUpdate(product);
                context.SaveChanges();
            }
            catch (Exception er)
            {
                //error
            }

            return View(product);
        }


        public ActionResult AddProduct(string id)
        {
            var context = new BooksEntities();
            Product product = context.Products.FirstOrDefault(x => x.ProductCode == id);

            if (product == null)
            {
                product = new Product();
            }

            return View(product);
        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            return View();
        }
    }
}