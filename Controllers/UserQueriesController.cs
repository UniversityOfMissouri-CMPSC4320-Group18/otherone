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
    public class UserQueriesController : Controller
    {
        private iClothingEntities1 db = new iClothingEntities1();

        // GET: UserQueries
        public ActionResult Index()
        {
            var userQuery = db.UserQuery.Include(u => u.Users);
            return View(userQuery.ToList());
        }

        // GET: UserQueries/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserQuery userQuery = db.UserQuery.Find(id);
            if (userQuery == null)
            {
                return HttpNotFound();
            }
            return View(userQuery);
        }

        // GET: UserQueries/Create
        public ActionResult Create()
        {
            ViewBag.queryUser = new SelectList(db.Users, "userID", "userName");
            return View();
        }

        // POST: UserQueries/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "queryNo,queryUser,queryDate,queryDescripion")] UserQuery userQuery)
        {
            if (ModelState.IsValid)
            {
                db.UserQuery.Add(userQuery);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.queryUser = new SelectList(db.Users, "userID", "userName", userQuery.queryUser);
            return View(userQuery);
        }

        // GET: UserQueries/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserQuery userQuery = db.UserQuery.Find(id);
            if (userQuery == null)
            {
                return HttpNotFound();
            }
            ViewBag.queryUser = new SelectList(db.Users, "userID", "userName", userQuery.queryUser);
            return View(userQuery);
        }

        // POST: UserQueries/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "queryNo,queryUser,queryDate,queryDescripion")] UserQuery userQuery)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userQuery).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.queryUser = new SelectList(db.Users, "userID", "userName", userQuery.queryUser);
            return View(userQuery);
        }

        // GET: UserQueries/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserQuery userQuery = db.UserQuery.Find(id);
            if (userQuery == null)
            {
                return HttpNotFound();
            }
            return View(userQuery);
        }

        // POST: UserQueries/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UserQuery userQuery = db.UserQuery.Find(id);
            db.UserQuery.Remove(userQuery);
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
