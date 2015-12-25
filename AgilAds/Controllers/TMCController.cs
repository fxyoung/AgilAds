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
    public class TMCController : ReqBaseController, IStackable
    {
        private AgilAdsDataContext db = new AgilAdsDataContext();
        private IUnitOfWorkAsync _uow;
        private const string idMsg = "TMC Controller";
        private stackFrame _frame;
        private Person person;
        public TMCController(IUnitOfWorkAsync uow) 
            : base(uow, stackFrame.stackContext.teamMemberContact)
        {
            _uow = uow;
            _frame = stackFrame.PeekContext(_currentContext);
            GetRoot().Wait();
            ViewBag.OrganizationName = person.Business.OrganizationName;
            ViewBag.Person = person.Fullname;
            ViewBag.CallerId = _frame.callerId;
        }
        private async Task GetRoot()
        {
            person = await db.People.SingleAsync(
                p => p.id == (int)_frame.param).ConfigureAwait(continueOnCapturedContext:false);
        }

        // GET: Contact
        public ActionResult Index()
        {
            return View(person.Contacts.ToList());
        }

        // GET: Contact/Details/5
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

        // GET: Contact/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Contact/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,Method,Contact")] PersonalContact contactInfo)
        {
            if (ModelState.IsValid)
            {
                db.ContactInfoes.Add(contactInfo);
                person.Contacts.Add(contactInfo);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(contactInfo);
        }

        // GET: Contact/Edit/5
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

        // POST: Contact/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,Method,Contact")] ContactInfo contactInfo)
        {
            if (ModelState.IsValid)
            {
                db.Entry(contactInfo).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(contactInfo);
        }

        // GET: Contact/Delete/5
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

        // POST: Contact/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            var contactInfo = await db.ContactInfoes.FindAsync(id);
            var personalContact = (PersonalContact)contactInfo;
            db.ContactInfoes.Remove(contactInfo);
            person.Contacts.Remove(personalContact);
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
