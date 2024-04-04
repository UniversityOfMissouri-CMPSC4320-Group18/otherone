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
    public class OrderStatusController : Controller
    {
        private iClothingEntities1 db = new iClothingEntities1();

        // GET: OrderStatus
        public ActionResult Index()
        {
            var orderStatus = db.OrderStatus.Include(o => o.Customers).Include(o => o.ItemDelivery1);
            return View(orderStatus.ToList());
        }

        // GET: OrderStatus/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderStatus orderStatus = db.OrderStatus.Find(id);
            if (orderStatus == null)
            {
                return HttpNotFound();
            }
            return View(orderStatus);
        }

        // GET: OrderStatus/Create
        public ActionResult Create()
        {
            ViewBag.orderCustomer = new SelectList(db.Customers, "customerID", "customerName");
            ViewBag.itemDelivery = new SelectList(db.ItemDelivery, "stickerID", "stickerID");
            return View();
        }

        // POST: OrderStatus/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "statusID,orderCustomer,status,statusDate,itemDelivery")] OrderStatus orderStatus)
        {
            if (ModelState.IsValid)
            {
                db.OrderStatus.Add(orderStatus);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.orderCustomer = new SelectList(db.Customers, "customerID", "customerName", orderStatus.orderCustomer);
            ViewBag.itemDelivery = new SelectList(db.ItemDelivery, "stickerID", "stickerID", orderStatus.itemDelivery);
            return View(orderStatus);
        }

        // GET: OrderStatus/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderStatus orderStatus = db.OrderStatus.Find(id);
            if (orderStatus == null)
            {
                return HttpNotFound();
            }
            ViewBag.orderCustomer = new SelectList(db.Customers, "customerID", "customerName", orderStatus.orderCustomer);
            ViewBag.itemDelivery = new SelectList(db.ItemDelivery, "stickerID", "stickerID", orderStatus.itemDelivery);
            return View(orderStatus);
        }

        // POST: OrderStatus/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "statusID,orderCustomer,status,statusDate,itemDelivery")] OrderStatus orderStatus)
        {
            if (ModelState.IsValid)
            {
                db.Entry(orderStatus).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.orderCustomer = new SelectList(db.Customers, "customerID", "customerName", orderStatus.orderCustomer);
            ViewBag.itemDelivery = new SelectList(db.ItemDelivery, "stickerID", "stickerID", orderStatus.itemDelivery);
            return View(orderStatus);
        }

        // GET: OrderStatus/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OrderStatus orderStatus = db.OrderStatus.Find(id);
            if (orderStatus == null)
            {
                return HttpNotFound();
            }
            return View(orderStatus);
        }

        // POST: OrderStatus/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            OrderStatus orderStatus = db.OrderStatus.Find(id);
            db.OrderStatus.Remove(orderStatus);
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
