using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Threading.Tasks;
using System.Net;
using System.Web;
using System.Web.Mvc;
using AgilAds.DAL;
using AgilAds.Models;
using AgilAds.Helpers;

namespace AgilAds.Controllers
{
    public class BICController : ReqBaseController, IStackable
    {
        private AgilAdsDataContext db = new AgilAdsDataContext();
        private IUnitOfWorkAsync _uow;
        private stackFrame _frame;
        private BusinessInfo bi;
        public BICController(IUnitOfWorkAsync uow)
            : base(uow, stackFrame.stackContext.businessInfoContact)
        {
            _uow = uow;
            _frame = stackFrame.PeekContext();
            GetRoot().Wait();
            ViewBag.OrganizationName = bi.OrganizationName;
            ViewBag.CallerId = _frame.callerId;
        }
        private async Task GetRoot()
        {
            bi = await db.BusinessInfoes.SingleAsync(
                b => b.id == (int)_frame.param).ConfigureAwait(continueOnCapturedContext: false);
        }
        // GET: BIC
        public ActionResult Index()
        {
            return View(bi.Contacts.ToList());
        }

        // GET: BIC/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactInfo contactInfo = await db.ContactInfoes.FindAsync(id);
            if (contactInfo == null)
            {
                return HttpNotFound();
            }
            return View(contactInfo);
        }

        // GET: BIC/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BIC/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,Method,Contact,BusinessInfoId")] BusinessContact businessContact)
        {
            if (ModelState.IsValid)
            {
                db.ContactInfoes.Add(businessContact);
                bi.Contacts.Add(businessContact);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(businessContact);
        }

        // GET: BIC/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactInfo contactInfo = await db.ContactInfoes.FindAsync(id);
            if (contactInfo == null)
            {
                return HttpNotFound();
            }
            return View(contactInfo);
        }

        // POST: BIC/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,Method,Contact,BusinessInfoId")] BusinessContact businessContact)
        {
            if (ModelState.IsValid)
            {
                db.Entry(businessContact).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(businessContact);
        }

        // GET: BIC/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            ContactInfo contactInfo = await db.ContactInfoes.FindAsync(id);
            if (contactInfo == null)
            {
                return HttpNotFound();
            }
            return View(contactInfo);
        }

        // POST: BIC/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            ContactInfo contactInfo = await db.ContactInfoes.FindAsync(id);
            db.ContactInfoes.Remove(contactInfo);
            bi.Contacts.Remove((BusinessContact)contactInfo);
            await db.SaveChangesAsync();
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
