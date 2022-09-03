using DBProgramming_II.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBProgramming_II.Controllers
{
    public class InvoicesController : Controller
    {
        // GET: Invoices
        public ActionResult Index(string id)
        {
            var context = new BooksEntities();
            List<int> invoiceLineItems = context.InvoiceLineItems.Where(i => i.ProductCode == id).Select(x => x.InvoiceID).ToList();

            // get all invoices based on product code
            List<Invoice> invoices = context.Invoices.Where(i => invoiceLineItems.Contains(i.InvoiceID)).ToList();
            return View(invoices);
        }

        public ActionResult AddInvoice(int id = 0)
        {
            var context = new BooksEntities();

            Invoice invoice = context.Invoices.FirstOrDefault(x => x.CustomerID == id);

            return View(invoice);
        }

        [HttpPost]
        public ActionResult AddInvoice(Invoice invoice)
        {
            var context = new BooksEntities();

            if (invoice.InvoiceID == 0)
            {
                Random random = new Random();
                invoice.InvoiceID = random.Next(1, 30000000);
            }

            try
            {
                context.Invoices.AddOrUpdate(invoice);
                context.SaveChanges();
                ViewBag.Msg = "Thankyou for submitting form";
            }
            catch (Exception)
            {
                //error
            }

            return View(invoice);
        }

    }
}