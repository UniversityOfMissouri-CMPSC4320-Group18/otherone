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
    public class UserCommentsController : Controller
    {
        private iClothingEntities1 db = new iClothingEntities1();

        // GET: UserComments
        public ActionResult Index()
        {
            var userComments = db.UserComments.Include(u => u.Users);
            return View(userComments.ToList());
        }

        // GET: UserComments/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserComments userComments = db.UserComments.Find(id);
            if (userComments == null)
            {
                return HttpNotFound();
            }
            return View(userComments);
        }

        // GET: UserComments/Create
        public ActionResult Create()
        {
            ViewBag.commentUser = new SelectList(db.Users, "userID", "userName");
            return View();
        }

        // POST: UserComments/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "commentNo,commentUser,commentDate,commentDescription")] UserComments userComments)
        {
            if (ModelState.IsValid)
            {
                db.UserComments.Add(userComments);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.commentUser = new SelectList(db.Users, "userID", "userName", userComments.commentUser);
            return View(userComments);
        }

        // GET: UserComments/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserComments userComments = db.UserComments.Find(id);
            if (userComments == null)
            {
                return HttpNotFound();
            }
            ViewBag.commentUser = new SelectList(db.Users, "userID", "userName", userComments.commentUser);
            return View(userComments);
        }

        // POST: UserComments/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "commentNo,commentUser,commentDate,commentDescription")] UserComments userComments)
        {
            if (ModelState.IsValid)
            {
                db.Entry(userComments).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.commentUser = new SelectList(db.Users, "userID", "userName", userComments.commentUser);
            return View(userComments);
        }

        // GET: UserComments/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            UserComments userComments = db.UserComments.Find(id);
            if (userComments == null)
            {
                return HttpNotFound();
            }
            return View(userComments);
        }

        // POST: UserComments/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            UserComments userComments = db.UserComments.Find(id);
            db.UserComments.Remove(userComments);
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
