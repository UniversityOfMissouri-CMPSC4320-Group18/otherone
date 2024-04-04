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
    public class ShoppingCartsController : Controller
    {
        private iClothingEntities1 db = new iClothingEntities1();

        // GET: ShoppingCarts
        public ActionResult Index()
        {
            return View(db.ShoppingCarts.ToList());
        }


        [HttpPost]
        public static void AddItem(String productID) // Access variable with request.VariableName
        {

            iClothingEntities1 db = new iClothingEntities1();
            WebApplication1.Models.ShoppingCartItemBridge newitem = new WebApplication1.Models.ShoppingCartItemBridge();
            newitem.productID = productID;
            newitem.cartID = WebApplication1.Controllers.UsersController.customerID;
            newitem.bridgeID = WebApplication1.Controllers.UsersController.GetRandomHexNumber(10);
            db.ShoppingCartItemBridge.Add(newitem);
            db.SaveChanges();

        }














        // GET: ShoppingCarts/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCarts shoppingCarts = db.ShoppingCarts.Find(id);
            if (shoppingCarts == null)
            {
                return HttpNotFound();
            }
            return View(shoppingCarts);
        }

        // GET: ShoppingCarts/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: ShoppingCarts/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "cartID,cartProductPrice,cartProductQty")] ShoppingCarts shoppingCarts)
        {
            if (ModelState.IsValid)
            {
                db.ShoppingCarts.Add(shoppingCarts);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            return View(shoppingCarts);
        }

        // GET: ShoppingCarts/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCarts shoppingCarts = db.ShoppingCarts.Find(id);
            if (shoppingCarts == null)
            {
                return HttpNotFound();
            }
            return View(shoppingCarts);
        }

        // POST: ShoppingCarts/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "cartID,cartProductPrice,cartProductQty")] ShoppingCarts shoppingCarts)
        {
            if (ModelState.IsValid)
            {
                db.Entry(shoppingCarts).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(shoppingCarts);
        }

        // GET: ShoppingCarts/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ShoppingCarts shoppingCarts = db.ShoppingCarts.Find(id);
            if (shoppingCarts == null)
            {
                return HttpNotFound();
            }
            return View(shoppingCarts);
        }

        // POST: ShoppingCarts/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            ShoppingCarts shoppingCarts = db.ShoppingCarts.Find(id);
            db.ShoppingCarts.Remove(shoppingCarts);
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
