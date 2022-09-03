using DBProgrammingClass_III.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBProgrammingClass_III.Controllers
{
    public class StateController : Controller
    {
        // GET: State
        public ActionResult StateList()
        {
            var context = new TechSupportEntities();

            List<State> state = context.States.ToList();

            string searchByStateName = Request.QueryString.Get("searchByStateName");

            if (!string.IsNullOrWhiteSpace(searchByStateName))
            {
                state = state.Where(x => x.StateName.ToLower().IndexOf(searchByStateName) != -1).ToList();
            }

            return View(state);
        }

        public ActionResult StateDetails(string id)
        {
            var context = new TechSupportEntities();
            State state = context.States.FirstOrDefault(x => x.StateCode == id);

            return PartialView(state);
        }

        [HttpPost]
        public ActionResult AddState(State state)
        {
            var context = new TechSupportEntities();

            try
            {
                context.States.AddOrUpdate(state);
                context.SaveChanges();
            }
            catch (Exception er)
            {
                //error
            }

            return Redirect("/State/StateList");
        }

        public ActionResult AddState(string id)
        {
            var context = new TechSupportEntities();
            State state = context.States.FirstOrDefault(x => x.StateCode == id);

            if (state == null)
            {
                state = new State();
            }

            return View(state);
        }

        public ActionResult Delete(string id)
        {
            var context = new TechSupportEntities();
            List<State> states = context.States.ToList();
            var stateToRemove = states.FirstOrDefault(x => x.StateCode == id);
            try
            {
                context.States.Remove(stateToRemove);
                context.SaveChanges();
            }
            catch (Exception ex)
            {
                //error
            }
            return Redirect("/State/StateList");
        }
    }
}