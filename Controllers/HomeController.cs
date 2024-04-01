using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class HomeController : Controller
    {
        public ActionResult Index()
        {
            iClothingEntities entities = new iClothingEntities();
            var customer = entities.Customers.SqlQuery("Select * FROM CUSTOMERS");
            Console.WriteLine(customer);

            return View(customer);

        }

        public ActionResult About()
        {
            ViewBag.Message = "Your application description page.";

            return View();
        }

        public ActionResult Contact()
        {
            ViewBag.Message = "Your contact page.";

            iClothingEntities entities = new iClothingEntities();
            var customer = entities.Customers.SqlQuery("Select * FROM CUSTOMERS");
            Console.WriteLine(customer);

            return View(customer);

 
        }
    }
}