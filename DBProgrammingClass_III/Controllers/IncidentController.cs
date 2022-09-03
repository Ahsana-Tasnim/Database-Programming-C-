using DBProgrammingClass_III.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBProgrammingClass_III.Controllers
{
    public class IncidentController : Controller
    {
        // GET: Incident
        public ActionResult IncidentList()
        {
            var context = new TechSupportEntities();

            List<Incident> incident = context.Incidents.ToList();

            string searchByTitle = Request.QueryString.Get("searchByTitle");

            if (!string.IsNullOrWhiteSpace(searchByTitle))
            {
                incident = incident.Where(x => x.Title.ToLower().IndexOf(searchByTitle) != -1).ToList();
            }

            return View(incident);
        }

        [HttpPost]
        public ActionResult AddIncident(Incident inc)
        {
            var context = new TechSupportEntities();

            if (inc.IncidentID == 0)
            {
                Random random = new Random();
                inc.IncidentID = random.Next(1, 30000000);
            }
            try
            {
                context.Incidents.AddOrUpdate(inc);
                context.SaveChanges();
            }
            catch (Exception er)
            {
                //error
            }

            return Redirect("/Incident/IncidentList");
        }

        public ActionResult AddIncident(int id = 0)
        {
            var context = new TechSupportEntities();
            Incident inc = context.Incidents.FirstOrDefault(x => x.IncidentID == id);

            if (inc == null)
            {
                inc = new Incident();
            }

            return View(inc);
        }

        public ActionResult IncidentDetails(int id)
        {
            var context = new TechSupportEntities();
            Incident inc = context.Incidents.FirstOrDefault(x => x.IncidentID == id);

            return PartialView(inc);
        }

        public ActionResult Delete(int id)
        {
            var context = new TechSupportEntities();
            List<Incident> incs = context.Incidents.ToList();
            var incToRemove = incs.FirstOrDefault(x => x.IncidentID == id);
            try
            {
                context.Incidents.Remove(incToRemove);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                //error
            }
            return Redirect("/Incident/IncidentList");
        }
    }
}
