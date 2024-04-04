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
    public class DepartmentItemBridgesController : Controller
    {
        private iClothingEntities1 db = new iClothingEntities1();

        // GET: DepartmentItemBridges
        public ActionResult Index()
        {
            var departmentItemBridge = db.DepartmentItemBridge.Include(d => d.Department).Include(d => d.Product);
            return View(departmentItemBridge.ToList());
        }

        // GET: DepartmentItemBridges/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentItemBridge departmentItemBridge = db.DepartmentItemBridge.Find(id);
            if (departmentItemBridge == null)
            {
                return HttpNotFound();
            }
            return View(departmentItemBridge);
        }

        // GET: DepartmentItemBridges/Create
        public ActionResult Create()
        {
            ViewBag.departmentID = new SelectList(db.Department, "departmentID", "departmentName");
            ViewBag.productID = new SelectList(db.Product, "productID", "productName");
            return View();
        }

        // POST: DepartmentItemBridges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bridgeID,departmentID,productID")] DepartmentItemBridge departmentItemBridge)
        {
            if (ModelState.IsValid)
            {
                db.DepartmentItemBridge.Add(departmentItemBridge);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.departmentID = new SelectList(db.Department, "departmentID", "departmentName", departmentItemBridge.departmentID);
            ViewBag.productID = new SelectList(db.Product, "productID", "productName", departmentItemBridge.productID);
            return View(departmentItemBridge);
        }

        // GET: DepartmentItemBridges/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentItemBridge departmentItemBridge = db.DepartmentItemBridge.Find(id);
            if (departmentItemBridge == null)
            {
                return HttpNotFound();
            }
            ViewBag.departmentID = new SelectList(db.Department, "departmentID", "departmentName", departmentItemBridge.departmentID);
            ViewBag.productID = new SelectList(db.Product, "productID", "productName", departmentItemBridge.productID);
            return View(departmentItemBridge);
        }

        // POST: DepartmentItemBridges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bridgeID,departmentID,productID")] DepartmentItemBridge departmentItemBridge)
        {
            if (ModelState.IsValid)
            {
                db.Entry(departmentItemBridge).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.departmentID = new SelectList(db.Department, "departmentID", "departmentName", departmentItemBridge.departmentID);
            ViewBag.productID = new SelectList(db.Product, "productID", "productName", departmentItemBridge.productID);
            return View(departmentItemBridge);
        }

        // GET: DepartmentItemBridges/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            DepartmentItemBridge departmentItemBridge = db.DepartmentItemBridge.Find(id);
            if (departmentItemBridge == null)
            {
                return HttpNotFound();
            }
            return View(departmentItemBridge);
        }

        // POST: DepartmentItemBridges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            DepartmentItemBridge departmentItemBridge = db.DepartmentItemBridge.Find(id);
            db.DepartmentItemBridge.Remove(departmentItemBridge);
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
