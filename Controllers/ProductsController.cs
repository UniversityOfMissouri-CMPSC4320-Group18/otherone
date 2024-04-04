using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Data.Entity.Infrastructure;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using System.Xml.Linq;
using WebApplication1.Models;

namespace WebApplication1.Controllers
{
    public class ProductsController : Controller
    {
        private iClothingEntities1 db = new iClothingEntities1();

        // GET: Products


        public ActionResult Index()
        {
            var product = db.Product.Include(p => p.Brand).Include(p => p.Category);
            return View(product.ToList());
        }


        public Boolean haswhere(String str)
        {
            if (str.Contains("WHERE"))
            {
                return true;
            }
            else
            {
                return false;
            }
        }



        [HttpPost]
        public ActionResult AddItem()
        {


            WebApplication1.Models.ShoppingCartItemBridge bridge = new WebApplication1.Models.ShoppingCartItemBridge();
     
            bridge.cartID = WebApplication1.Controllers.UsersController.userCart;
            bridge.productID = Request.QueryString["variableName"];
            bridge.bridgeID = WebApplication1.Controllers.UsersController.GetRandomHexNumber(10);
            db.ShoppingCartItemBridge.Add(bridge);
            db.SaveChanges();
            return Content("HI");
        }



        [HttpPost]
        public ActionResult SayHi()
        {
            return Content("HI");
        }



        public ActionResult Men(String name, String productCategory, String productBrand)
        {

            var category = db.Category.SqlQuery("Select * FROM CATEGORY");
            ViewBag.Category = category;
            var brand = db.Brand.SqlQuery("Select * FROM BRAND");
            ViewBag.Brand = brand;

            String querystring = "Select * FROM PRODUCT";
            
            if (string.IsNullOrEmpty(name) == false)
            {
                if (haswhere(querystring))
                {
                    querystring = querystring + " AND ";
                }
                else
                {
                    querystring = querystring + " WHERE ";
                }
                querystring = querystring + "productName LIKE '%" + name + "%'";
   
            }
            if(string.IsNullOrEmpty(productCategory) == false)
            {

                if (haswhere(querystring))
                {
                    querystring = querystring + " AND ";
                }
                else
                {
                    querystring = querystring + " WHERE ";
                }
                querystring = querystring + "productCategory = '" + productCategory + "'";
            }
            if (string.IsNullOrEmpty(productBrand) == false)
            {

                if (haswhere(querystring))
                {
                    querystring = querystring + " AND ";
                }
                else
                {
                    querystring = querystring + " WHERE ";
                }
                querystring = querystring + "productBrand = '" + productBrand + "'";
            }

            var product = db.Product.SqlQuery(querystring);
            return View(product);




        }
        public ActionResult Women(String name)
        {

            var category = db.Category.SqlQuery("Select * FROM CATEGORY");
            ViewBag.Category = category;

            if (string.IsNullOrEmpty(name)) 
            {
                var product = db.Product.SqlQuery("Select * FROM PRODUCT");

                return View(product);
            }
            else
            {
                var products = db.Product
    .Include(p => p.Brand)  // Include Brand information
    .Include(p => p.Category) // Include Category information
    .Where(p => p.productName.ToUpper().Contains(name.ToUpper())) // Filter by product name (existing logic)
    .Join( // Join products with departmentItemBridge
        db.DepartmentItemBridge,
        p => p.productID,
        dib => dib.productID,
        (p, dib) => new { Product = p, Bridge = dib }  // Create anonymous type
    )
    .Join( // Join departmentItemBridge with Department to filter by department name
        db.Department,
        bridgeAndProduct => bridgeAndProduct.Bridge.departmentID,
        d => d.departmentID,
        (bridgeAndProduct, d) => new { Product = bridgeAndProduct.Product, Department = d }  // Create another anonymous type
    )
    .Where(bridgeProductDepartment => bridgeProductDepartment.Department.departmentName == "m")  // Filter by department name 'm'
    .Select( // Select only the Product
        bridgeProductDepartment => bridgeProductDepartment.Product
    )
    .ToList();
                return View(products);
            }




        }






        // GET: Products/Details/5
        public ActionResult Details(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // GET: Products/Create
        public ActionResult Create()
        {
            ViewBag.productBrand = new SelectList(db.Brand, "brandID", "brandName");
            ViewBag.productCategory = new SelectList(db.Category, "categoryID", "categoryName");
            return View();
        }

        // POST: Products/Create
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.

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



        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "productName,productDescription,productPrice,productQuantity,productBrand,productImage,productCategory")] Product product, String departmentName)
        {
            if (ModelState.IsValid)
            {
                String productID = GetRandomHexNumber(10);
                product.productID = productID;
                db.Product.Add(product);
                db.SaveChanges();
                DepartmentItemBridge bridge = new DepartmentItemBridge();
                bridge.productID = productID;
                bridge.departmentID = departmentName;
                bridge.bridgeID = GetRandomHexNumber(10);
                db.DepartmentItemBridge.Add(bridge);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.productBrand = new SelectList(db.Brand, "brandID", "brandName", product.productBrand);
            ViewBag.productCategory = new SelectList(db.Category, "categoryID", "categoryName", product.productCategory);
            return View(product);
        }

        // GET: Products/Edit/5
        public ActionResult Edit(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            ViewBag.productBrand = new SelectList(db.Brand, "brandID", "brandName", product.productBrand);
            ViewBag.productCategory = new SelectList(db.Category, "categoryID", "categoryName", product.productCategory);
            return View(product);
        }

        // POST: Products/Edit/5
        // To protect from overposting attacks, enable the specific properties you want to bind to, for 
        // more details see https://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "productID,productName,productDescription,productPrice,productQuantity,productBrand,productImage,productCategory")] Product product)
        {
            if (ModelState.IsValid)
            {
                db.Entry(product).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.productBrand = new SelectList(db.Brand, "brandID", "brandName", product.productBrand);
            ViewBag.productCategory = new SelectList(db.Category, "categoryID", "categoryName", product.productCategory);
            return View(product);
        }

        // GET: Products/Delete/5
        public ActionResult Delete(string id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Product product = db.Product.Find(id);
            if (product == null)
            {
                return HttpNotFound();
            }
            return View(product);
        }

        // POST: Products/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(string id)
        {
            Product product = db.Product.Find(id);
            db.Product.Remove(product);
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
