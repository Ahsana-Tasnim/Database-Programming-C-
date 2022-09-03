using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;

namespace DBProgramming_II.Models
{
    public class AllDataLists
    {
        public List<State> States { get; set; }
        public List<Product> Products { get; set; }
        public List<Customer> Customers { get; set; }
        //public List<Invoice> Invoices { get; set; }
        public Invoice Invoice { get; set; }
        public List<InvoiceLineItem> invoiceLineItems { get; set; }
    }
}