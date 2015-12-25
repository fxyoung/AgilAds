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
    public class BITController : ReqBaseController, IStackable
    {
        private const string idMsg = "BIT Controller";
        private AgilAdsDataContext db = new AgilAdsDataContext();
        private IUnitOfWorkAsync _uow;
        private stackFrame _frame;
        private BusinessInfo bi;
        public BITController(IUnitOfWorkAsync uow) : base(uow, stackFrame.stackContext.businessInfoTeam)
        {
            _uow = uow;
            _frame = stackFrame.PeekContext(_currentContext);
            GetRoot().Wait();
            ViewBag.OrganizationName = bi.OrganizationName;
            ViewBag.CallerId = _frame.callerId;
        }
        private async Task GetRoot()
        {
            bi = await db.BusinessInfoes.SingleOrDefaultAsync(
                b => b.id == (int)_frame.param).ConfigureAwait(continueOnCapturedContext:false);
        }

        // GET: BIT
        public ActionResult Index()
        {
               return View(bi.Team.ToList());
        }

        public ActionResult Contact(int id)
        {
            var route = stackFrame.Invoke(stackFrame.stackContext.teamMemberContact, id, idMsg);
            return RedirectToRoute(route);
        }

        // GET: BIT/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = await db.People.FindAsync(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // GET: BIT/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: BIT/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,Firstname,Lastname,Username")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.People.Add(person);
                bi.Team.Add(person);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: BIT/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = await db.People.FindAsync(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: BIT/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,Fullname,Firstname,Lastname,Username")] Person person)
        {
            if (ModelState.IsValid)
            {
                db.Entry(person).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            return View(person);
        }

        // GET: BIT/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Person person = await db.People.FindAsync(id);
            if (person == null)
            {
                return HttpNotFound();
            }
            return View(person);
        }

        // POST: BIT/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Person person = await db.People.FindAsync(id);
            bi.Team.Remove(person);
            db.People.Remove(person);
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
