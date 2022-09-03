using DBProgrammingClass_III.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBProgrammingClass_III.Controllers
{
    public class RegistrationController : Controller
    {
        // GET: Registration
        public ActionResult RegList()
        {
            var context = new TechSupportEntities();

            List<Registration> reg = context.Registrations.ToList();

            string searchRegByCusId = Request.QueryString.Get("searchRegByCusId");

            //if (!string.IsNullOrWhiteSpace(searchRegByCusId))
            //{
            //    reg = reg.Where(x => x.CustomerID.searchRegByCusId != -1).ToList();
            //}

            return View(reg);
        }

        public ActionResult RegDetails(int id)
        {
            var context = new TechSupportEntities();
            Registration reg = context.Registrations.FirstOrDefault(x => x.CustomerID == id);

            return PartialView(reg);
        }

        [HttpPost]
        public ActionResult AddReg(Registration reg)
        {
            var context = new TechSupportEntities();

            try
            {
                context.Registrations.AddOrUpdate(reg);
                context.SaveChanges();
            }
            catch (Exception er)
            {
                //error
            }

            return Redirect("/Registration/RegList");
        }

        public ActionResult AddReg(int id = 0)
        {
            var context = new TechSupportEntities();
            Registration reg = context.Registrations.FirstOrDefault(x => x.CustomerID == id);

            if (reg == null)
            {
                reg = new Registration();
            }

            return View(reg);
        }

        public ActionResult Delete(int id)
        {
            var context = new TechSupportEntities();
            List<Registration> regs = context.Registrations.ToList();
            var regsToRemove = regs.FirstOrDefault(x => x.CustomerID == id);
            try
            {
                context.Registrations.Remove(regsToRemove);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                //error
            }
            return Redirect("/Registration/RegList");
        }
    }
}