using DBProgramming_II.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBProgramming_II.Controllers
{
    public class CustomersController : Controller
    {
        // GET: Customers
        public ActionResult Index(int id)
        {
            var context = new BooksEntities();

            if (id == 0)
            {
                return Redirect("/Customers/AddCustomer");
            }

            Customer customer = context.Customers.FirstOrDefault(x => x.CustomerID == id);
            return View(customer);
        }

        public ActionResult AddCustomer(int id = 0)
        {
            var context = new BooksEntities();
            Customer customer = context.Customers.FirstOrDefault(x => x.CustomerID == id);

            if (customer == null)
            {
                customer = new Customer();
            }

            return View(customer);
        }

        [HttpPost]
        public ActionResult AddCustomer(Customer customer)
        {
            var context = new BooksEntities();

            if(customer.CustomerID == 0)
            {
                Random random = new Random();
                customer.CustomerID = random.Next(1, 30000000);
            }

            try
            {
                context.Customers.AddOrUpdate(customer);
                context.SaveChanges();
            }
            catch (Exception er)
            {
                //error
            }

            return View(customer);
        }

        public ActionResult CustomerList(string searchByName)
        {
            var context = new BooksEntities();
            List<Customer> customers = context.Customers.ToList();

            if (!string.IsNullOrWhiteSpace(searchByName))
            {
                customers = customers
                    .Where(x =>
                        x.Name.ToLower().IndexOf(searchByName) != -1
                     ).ToList();
            }

            return View(customers);
        }

        public ActionResult Delete(int id)
        {
            return Redirect("/Customers/CustomerList");
        }
    }
}