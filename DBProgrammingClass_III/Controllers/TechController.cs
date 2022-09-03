using DBProgrammingClass_III.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBProgrammingClass_III.Controllers
{
    public class TechController : Controller
    {
        // GET: Tech
        public ActionResult TechList()
        {
            var context = new TechSupportEntities();

            List<Technician> tech = context.Technicians.ToList();

            string searchByTechName = Request.QueryString.Get("searchByTechName");

            if (!string.IsNullOrWhiteSpace(searchByTechName))
            {
               tech = tech.Where(x => x.Name.ToLower().IndexOf(searchByTechName) != -1).ToList();
            }

            return View(tech);
        }

        public ActionResult TechDetails(int id)
        {
            var context = new TechSupportEntities();
            Technician tech = context.Technicians.FirstOrDefault(x => x.TechID == id);

            return PartialView(tech);
        }

        [HttpPost]
        public ActionResult AddTech(Technician tech)
        {
            var context = new TechSupportEntities();

            if (tech.TechID == 0)
            {
                Random random = new Random();
                tech.TechID = random.Next(1, 30000000);
            }
            try
            {
                context.Technicians.AddOrUpdate(tech);
                context.SaveChanges();
            }
            catch (Exception er)
            {
                //error
            }

            return Redirect("/Tech/TechList");
        }

        public ActionResult AddTech(int id = 0)
        {
            var context = new TechSupportEntities();
            Technician tech = context.Technicians.FirstOrDefault(x => x.TechID == id);

            if (tech == null)
            {
                tech = new Technician();
            }

            return View(tech);
        }

        public ActionResult Delete(int id)
        {
            var context = new TechSupportEntities();
            List<Technician> techs = context.Technicians.ToList();
            var techToRemove = techs.FirstOrDefault(x => x.TechID == id);
            try
            {
                context.Technicians.Remove(techToRemove);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                //error
            }
            return Redirect("/Tech/TechList");
        }
    }
}
