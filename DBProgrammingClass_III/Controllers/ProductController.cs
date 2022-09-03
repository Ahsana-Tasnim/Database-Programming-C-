using DBProgrammingClass_III.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBProgrammingClass_III.Controllers
{
    public class ProductController : Controller
    {
        // GET: Product
        public ActionResult ProductList()
        {
            var context = new TechSupportEntities();

            List<Product> product = context.Products.ToList();

            string searchByProductName = Request.QueryString.Get("searchByProductName");

            if (!string.IsNullOrWhiteSpace(searchByProductName))
            {
                product = product.Where(x => x.Name.ToLower().IndexOf(searchByProductName) != -1).ToList();
            }

            return View(product);
        }


        [HttpPost]
        public ActionResult AddProduct(Product product)
        {
            var context = new TechSupportEntities();

            try
            {
                context.Products.AddOrUpdate(product);
                context.SaveChanges();
            }
            catch (Exception er)
            {
                //error
            }

            return Redirect("/Product/ProductList");
        }

        public ActionResult AddProduct(string id)
        {
            var context = new TechSupportEntities();
            Product product = context.Products.FirstOrDefault(x => x.ProductCode.Trim() == id);

            if (product == null)
            {
                product = new Product();
            }

            return View(product);
        }

        public ActionResult ProductDetails(string id)
        {
            var context = new TechSupportEntities();
            Product product = context.Products.FirstOrDefault(x => x.ProductCode == id);

            return PartialView(product);
        }

        public ActionResult Delete(string id)
        {
            var context = new TechSupportEntities();
            List<Product> products = context.Products.ToList();
            var productToRemove = products.FirstOrDefault(x => x.ProductCode == id);
            try
            {
                context.Products.Remove(productToRemove);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                //error
            }
            return Redirect("/Product/ProductList");
        }
    }

}
