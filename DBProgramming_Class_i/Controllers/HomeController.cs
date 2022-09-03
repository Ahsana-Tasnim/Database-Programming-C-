using DBProgramming_Class_i.Models;
using System;
using System.Collections.Generic;
using System.Data.Entity.Migrations;
using System.Linq;
using System.Web;
using System.Web.Mvc;

namespace DBProgramming_Class_i.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            // init context
            var context = new CompanyEntities();

            /* ==
               !=
               <>
               IndexOf
               Contains
             */

            //creating List of employees
            List<Employee> employees = context.Employees
                .Where(x => 
                    (x.Last_Name.ToLower().IndexOf("b") == -1) &&
                    x.First_Name != null && 
                    x.Dept_Id !=null)
                .OrderBy(x => 
                    x.First_Name)
                .ToList();

            string searchTermFName = Request.QueryString.Get("searchTermFName");
            string searchTermLName = Request.QueryString.Get("searchTermLName");

            if (!string.IsNullOrWhiteSpace(searchTermFName))
            {
                //List<string> andSearchTerm = searchTerm.Split('+').ToList();
                employees = employees
                    .Where(x =>
                        x.First_Name.ToLower().IndexOf(searchTermFName) != -1
                     ).ToList();
            }

            if (!string.IsNullOrWhiteSpace(searchTermLName))
            {
                employees = employees
                    .Where(x =>
                        x.Last_Name.ToLower().IndexOf(searchTermLName) != -1
                     ).ToList();
            }

            //creating List of departments
            List<Department> departments = context.Departments.OrderBy(x => x.Dept_Name).ToList();

            List<string> nameOfDepartments = context.Departments.Select(x => x.Dept_Name).ToList();

            //key = Id
            //value => Depo Name

            CombinedLists combinedLists = new CombinedLists();
            combinedLists.Department = departments;
            combinedLists.Employee = employees;


            return View(combinedLists);
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

        [HttpDelete]
        public ActionResult DeleteEmployee(string empId)
        {
            var context = new CompanyEntities();
            List<Employee> employees = context.Employees.ToList();
            //LINQ
            var employeeToRemove = employees.FirstOrDefault(x => x.Emp_Id.ToString() == empId);

            context.Employees.Remove(employeeToRemove);
            context.SaveChanges();

            return Json(true, JsonRequestBehavior.AllowGet);
        }

        [HttpPost]
        public ActionResult UpsertEmployee(Employee employee)
        {
            var context = new CompanyEntities();
            Random random = new Random();

            if(employee.Emp_Id != 0)
            {
                List<Employee> employees = context.Employees.ToList();
                Employee employeeToUpdate = (Employee)employees.FirstOrDefault(x => x.Emp_Id == employee.Emp_Id);

                employeeToUpdate.First_Name = employee.First_Name;
                employeeToUpdate.Last_Name = employee.Last_Name;
            }

            if(employee.Emp_Id == 0)
            {
                employee.Emp_Id = random.Next(1, 30000000);
                context.Employees.Add(employee);
            }

            try
            {
                context.SaveChanges();

                return Json(true, JsonRequestBehavior.AllowGet);
            }
            catch (Exception)
            {
                return Json(false, JsonRequestBehavior.AllowGet);
            }
        }
    }
}