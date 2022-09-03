using DBProgrammingClass_III.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBProgrammingClass_III.Controllers
{
    public class CustomerController : Controller
    {
        // GET: Customer
        public ActionResult CustomerList()
        {
            var context = new TechSupportEntities();

            List<Customer> customer = context.Customers.ToList();

            string searchByName = Request.QueryString.Get("searchByName");

            if (!string.IsNullOrWhiteSpace(searchByName))
            {
                customer = customer.Where(x =>x.Name.ToLower().IndexOf(searchByName) != -1).ToList();
            }

            return View(customer);
        }

        [HttpPost]
        public ActionResult AddCustomer(Customer customer)
        {
            var context = new TechSupportEntities();

            if (customer.CustomerID == 0)
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

            return Redirect("/Customer/CustomerList");
        }

        public ActionResult AddCustomer(int id = 0)
        {
            var context = new TechSupportEntities();
            Customer customer = context.Customers.FirstOrDefault(x => x.CustomerID == id);

            if (customer == null)
            {
                customer = new Customer();
            }

            return View(customer);
        }

        public ActionResult CustomerDetails(int id)
        {
            var context = new TechSupportEntities();
            Customer customer = context.Customers.FirstOrDefault(x => x.CustomerID == id);

            return PartialView(customer);
        }
    }


}