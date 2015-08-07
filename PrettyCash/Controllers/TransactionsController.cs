using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using PrettyCash.Models;
using Microsoft.AspNet.Identity;
using Microsoft.AspNet.Identity.Owin;
using Microsoft.Owin.Security;

namespace PrettyCash.Controllers
{
    [Authorize]
    public class TransactionsController : Controller
    {
        private ApplicationUserManager _userManager;

        public ApplicationUserManager UserManager
        {
            get
            {
                return _userManager ?? HttpContext.GetOwinContext().GetUserManager<ApplicationUserManager>();
            }
            private set
            {
                _userManager = value;
            }
        }

        private ApplicationDbContext db = new ApplicationDbContext();

        // GET: Transactions
        public ActionResult Index()
        {
            ViewBag.LogType = TempData["LogType"];
            ViewBag.LogMessage = TempData["LogMessage"];

            var userId = User.Identity.GetUserId();
            return View(db.Transactions.Where(t => t.CreatedBy.Id == userId).OrderByDescending(t => t.CreatedDateTime).ToList());
        }

        // GET: Transactions/Details/5
        public ActionResult Details(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // GET: Transactions/Create
        public ActionResult Create()
        {
            var user = db.Users.Find(User.Identity.GetUserId());
            var userCurrency = db.UserCurrency.Where(c => c.ApplicationUser.Id == user.Id);

            if(!userCurrency.Any())
            {
                TempData["LogType"] = 3;
                TempData["LogMessage"] = "Please make sure you setup your default currency under currency menu";
                return RedirectToAction("Index");
            }

            return View();
        }

        // POST: Transactions/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "Id, AmountMST, Notes")] Transaction transaction)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    transaction.Id = Guid.NewGuid();
                    transaction.CreatedDateTime = DateTime.UtcNow;
                    transaction.CreatedBy = db.Users.Find(User.Identity.GetUserId());
                    transaction.Currency = db.UserCurrency.Where(u => u.ApplicationUser.Id == transaction.CreatedBy.Id).First().Currency;

                    db.Transactions.Add(transaction);
                    db.SaveChanges();
                    return RedirectToAction("Index");
                }

                return View(transaction);
            }
            catch(Exception)
            {
                TempData["LogType"] = 4;
                TempData["LogMessage"] = "An error occurred, please contact site adminitrator, no transaction has been created";
                return RedirectToAction("Index");
            }
        }

        // GET: Transactions/Edit/5
        public ActionResult Edit(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "Id, AmountMST, AmountCur, CreatedDateTime, CreatedBy, ModifiedBy, ModifiedDateTime, Notes")] Transaction transaction)
        {
            if (ModelState.IsValid)
            {
                transaction.ModifiedDateTime = DateTime.UtcNow;
                transaction.ModifiedBy = db.Users.Find(User.Identity.GetUserId());
                transaction.Currency = db.UserCurrency.Where(u => u.ApplicationUser.Id == transaction.ModifiedBy.Id).First().Currency;

                db.Entry(transaction).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            return View(transaction);
        }

        // GET: Transactions/Delete/5
        public ActionResult Delete(Guid? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Transaction transaction = db.Transactions.Find(id);
            if (transaction == null)
            {
                return HttpNotFound();
            }
            return View(transaction);
        }

        // POST: Transactions/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(Guid id)
        {
            Transaction transaction = db.Transactions.Find(id);
            db.Transactions.Remove(transaction);
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
