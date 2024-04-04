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
    public class ShoppingCartItemBridgesController : Controller
    {
        private iClothingEntities1 db = new iClothingEntities1();

        // GET: ShoppingCartItemBridges
        public ActionResult Index()
        {
            var shoppingCartItemBridge = db.ShoppingCartItemBridge.Include(s => s.Product).Include(s => s.ShoppingCarts);
            return View(shoppingCartItemBridge.ToList());
        }

        // GET: ShoppingCartItemBridges/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCartItemBridge shoppingCartItemBridge = db.ShoppingCartItemBridge.Find(id);
            if (shoppingCartItemBridge == null)
            {
                return HttpNotFound();
            }
            return View(shoppingCartItemBridge);
        }

        // GET: ShoppingCartItemBridges/Create
        public ActionResult Create()
        {
            ViewBag.productID = new SelectList(db.Product, "productID", "productName");
            ViewBag.cartID = new SelectList(db.ShoppingCarts, "cartID", "cartID");
            return View();
        }

        // POST: ShoppingCartItemBridges/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "bridgeID,cartID,productID")] ShoppingCartItemBridge shoppingCartItemBridge)
        {
            if (ModelState.IsValid)
            {
                db.ShoppingCartItemBridge.Add(shoppingCartItemBridge);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.productID = new SelectList(db.Product, "productID", "productName", shoppingCartItemBridge.productID);
            ViewBag.cartID = new SelectList(db.ShoppingCarts, "cartID", "cartID", shoppingCartItemBridge.cartID);
            return View(shoppingCartItemBridge);
        }

        // GET: ShoppingCartItemBridges/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCartItemBridge shoppingCartItemBridge = db.ShoppingCartItemBridge.Find(id);
            if (shoppingCartItemBridge == null)
            {
                return HttpNotFound();
            }
            ViewBag.productID = new SelectList(db.Product, "productID", "productName", shoppingCartItemBridge.productID);
            ViewBag.cartID = new SelectList(db.ShoppingCarts, "cartID", "cartID", shoppingCartItemBridge.cartID);
            return View(shoppingCartItemBridge);
        }

        // POST: ShoppingCartItemBridges/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "bridgeID,cartID,productID")] ShoppingCartItemBridge shoppingCartItemBridge)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shoppingCartItemBridge).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.productID = new SelectList(db.Product, "productID", "productName", shoppingCartItemBridge.productID);
            ViewBag.cartID = new SelectList(db.ShoppingCarts, "cartID", "cartID", shoppingCartItemBridge.cartID);
            return View(shoppingCartItemBridge);
        }

        // GET: ShoppingCartItemBridges/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCartItemBridge shoppingCartItemBridge = db.ShoppingCartItemBridge.Find(id);
            if (shoppingCartItemBridge == null)
            {
                return HttpNotFound();
            }
            return View(shoppingCartItemBridge);
        }

        // POST: ShoppingCartItemBridges/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ShoppingCartItemBridge shoppingCartItemBridge = db.ShoppingCartItemBridge.Find(id);
            db.ShoppingCartItemBridge.Remove(shoppingCartItemBridge);
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
