using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    
    public class UsersController : Controller
    {
        public static Boolean isLoggedIn = false;
        public static Boolean isAdmin = false;
        public static string customerID = "";
        public static string userNameGlob = "";
        public static string userCart = "";
        private iClothingEntities1 db = new iClothingEntities1();

        // GET: Users
        public ActionResult Index()
        {
            var users = db.Users.Include(u => u.Customers).Include(u => u.ShoppingCarts);
            return View(users.ToList());
        }

        // GET: Users/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }


        static Random random = new Random();
        public static string GetRandomHexNumber(int digits)
        {
            byte[] buffer = new byte[digits / 2];
            random.NextBytes(buffer);
            string result = String.Concat(buffer.Select(x => x.ToString("X2")).ToArray());
            if (digits % 2 == 0)
                return result;
            return result + random.Next(16).ToString("X");
        }



        public ActionResult Register()
        {

            return View();
        }



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Register([Bind(Include = "userName,userPassword")] Users users)
        {
            if (ModelState.IsValid)
            {
                users.userID = GetRandomHexNumber(10);
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View();
        }







        public ActionResult Login()
        {

            return View();
        }



        [HttpPost]
        public ActionResult Login(string userName, string userPassword)
        {

            var user = db.Users.SqlQuery("SELECT * FROM Users WHERE userName = '" + userName + "' AND userPassword = '" + userPassword + "'");
            if (user.Any())
            {
                isLoggedIn = true;
                if(userName == "admin" && userPassword == "admin")
                {
                    isAdmin = true;
                }
                customerID = user.Take(1).ToList()[0].userCustomerData;
                if (user.Take(1).ToList()[0].userCart == null)
                {
                    ShoppingCarts newcart = new ShoppingCarts();
                    newcart.cartID = user.Take(1).ToList()[0].userID;
                    db.ShoppingCarts.Add(newcart);
                    user.Take(1).ToList()[0].userCart = newcart.cartID;
                    db.SaveChanges();
                }
            
                userCart = user.Take(1).ToList()[0].userCart;
                userNameGlob = userName;
                return RedirectToAction("Index", "Home");
            }
                return RedirectToAction("Login");
            

  
        }











        // GET: Users/Create
        public ActionResult Create()
        {
            ViewBag.userCustomerData = new SelectList(db.Customers, "customerID", "customerName");
            ViewBag.userCart = new SelectList(db.ShoppingCarts, "cartID", "cartID");
            return View();
        }

        // POST: Users/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "userID,userName,userPassword,userExpDate,userPasswordExpDate,userCustomerData,userCart")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Users.Add(users);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.userCustomerData = new SelectList(db.Customers, "customerID", "customerName", users.userCustomerData);
            ViewBag.userCart = new SelectList(db.ShoppingCarts, "cartID", "cartID", users.userCart);
            return View(users);
        }

        // GET: Users/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            ViewBag.userCustomerData = new SelectList(db.Customers, "customerID", "customerName", users.userCustomerData);
            ViewBag.userCart = new SelectList(db.ShoppingCarts, "cartID", "cartID", users.userCart);
            return View(users);
        }

        // POST: Users/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "userID,userName,userPassword,userExpDate,userPasswordExpDate,userCustomerData,userCart")] Users users)
        {
            if (ModelState.IsValid)
            {
                db.Entry(users).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.userCustomerData = new SelectList(db.Customers, "customerID", "customerName", users.userCustomerData);
            ViewBag.userCart = new SelectList(db.ShoppingCarts, "cartID", "cartID", users.userCart);
            return View(users);
        }

        // GET: Users/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Users users = db.Users.Find(id);
            if (users == null)
            {
                return HttpNotFound();
            }
            return View(users);
        }

        // POST: Users/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Users users = db.Users.Find(id);
            db.Users.Remove(users);
            db.SaveChanges();
            return RedirectToAction("Index");
        }

        protected override void Dispose(bool disposing)
        {
            if (disposing)
            {
                db.Dispose();
            }
            base.Dispose(disposing);
        }
    }
}
