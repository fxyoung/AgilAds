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
    public class PrvController : ReqBaseController, IStackable
    {
        private AgilAdsDataContext db = new AgilAdsDataContext();
        private IUnitOfWorkAsync _uow;
        private const string idMsg = "PRV Controller";
        private stackFrame _frame;
        private Person person;
        public PrvController(IUnitOfWorkAsync uow) : base(uow,stackFrame.stackContext.Priv)
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
                p => p.id == (int)_frame.param).ConfigureAwait(continueOnCapturedContext: false);
        }

        // GET: Prv
        public async Task<ActionResult> Index()
        {
            return View(await db.Privs.Where(p => p.Username == person.Username).ToListAsync());
        }

        // GET: Prv/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Priv priv = await db.Privs.FindAsync(id);
            if (priv == null)
            {
                return HttpNotFound();
            }
            return View(priv);
        }

        // GET: Prv/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Prv/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,Username,Context,Action")] Priv priv)
        {
            if (ModelState.IsValid)
            {
                db.Privs.Add(priv);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            return View(priv);
        }

        // GET: Prv/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Priv priv = await db.Privs.FindAsync(id);
            if (priv == null)
            {
                return HttpNotFound();
            }
            return View(priv);
        }

        // POST: Prv/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,Username,Context,Action")] Priv priv)
        {
            if (ModelState.IsValid)
            {
                db.Entry(priv).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(priv);
        }

        // GET: Prv/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Priv priv = await db.Privs.FindAsync(id);
            if (priv == null)
            {
                return HttpNotFound();
            }
            return View(priv);
        }

        // POST: Prv/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Priv priv = await db.Privs.FindAsync(id);
            db.Privs.Remove(priv);
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
