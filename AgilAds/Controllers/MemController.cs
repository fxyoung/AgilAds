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
    public class MemController : ReqBaseController, IStackable
    {
        private AgilAdsDataContext db = new AgilAdsDataContext();
        private IUnitOfWorkAsync _uow;
        private const string idMsg = "Mem Controller";
        private stackFrame _frame;
        private Rep rep;
        public MemController(IUnitOfWorkAsync uow)
            : base(uow, stackFrame.stackContext.Member)
        {
            _uow = uow;
            _frame = stackFrame.PeekContext(_currentContext);
            GetRoot().Wait();
            ViewBag.OrganizationName = rep.OrganizationName;
            ViewBag.CallerId = _frame.callerId;
        }
        private async Task GetRoot()
        {
            rep = await db.Reps.SingleAsync(
                r => r.id == (int)_frame.param).ConfigureAwait(continueOnCapturedContext:false);
        }

        // GET: Mem
        public ActionResult Index()
        {
            return View(MemListAllView.CreateListView(rep.Members.ToList()));
        }

        public ActionResult Team(int id)
        {
            var route = stackFrame.Invoke(stackFrame.stackContext.businessInfoTeam, id, idMsg);
            return RedirectToRoute(route);
        }

        public ActionResult Contact(int id)
        {
            var route = stackFrame.Invoke(stackFrame.stackContext.businessInfoContact, id, idMsg);
            return RedirectToRoute(route);
        }

        // GET: Mem/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = await db.Members.FindAsync(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Mem/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Mem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id," + Helpers.Constants.CreateViewCommonBindSpec + ",StaticMsg")] MemCreateView member)
        {
            if (ModelState.IsValid)
            {
                var nuMem = new Member(member);
                db.BusinessInfoes.Add(nuMem);
                await db.SaveChangesAsync();
                if (nuMem.Team.Count() == 1)
                {
                    nuMem.FocalPointId = nuMem.Team.First().id;
                    await db.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }
            return View(member);
        }

        // GET: Mem/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = await db.Members.FindAsync(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            ViewBag.FocalPoint = new SelectList(member.Team, "id", "FullName", member.FocalPointId);
            return View(member);
        }

        // POST: Mem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,OrganizationName,BankAcctNo,FocalPointId,ArcSum,Secret,ParentId,StaticMsg")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FocalPoint = new SelectList(member.Team, "id", "FullName", member.FocalPointId);
            return View(member);
        }

        // GET: Mem/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = await db.Members.FindAsync(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // POST: Mem/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Member member = await db.Members.FindAsync(id);
            db.BusinessInfoes.Remove(member);
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
