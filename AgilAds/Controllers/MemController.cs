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

namespace AgilAds.Controllers
{
    public class MemController : Controller
    {
        private AgilAdsDataContext db = new AgilAdsDataContext();
        private Rep rep;
        private int repId;
        public MemController()
        {
            repId = 1;
            GetRep();
        }
        private async void GetRep()
        {
            var task = db.Rep.FindAsync(repId);
            rep = await task;
        }

        // GET: Mem
        public ActionResult Index()
        {
            var members = rep.Members.ToList() ;
            return View( members );
        }

        // GET: Mem/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = await db.BusinessInfoes.FindAsync(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            return View(member);
        }

        // GET: Mem/Create
        public ActionResult Create()
        {
            ViewBag.FocalPointId = new SelectList(db.People, "id", "Fullname");
            return View();
        }

        // POST: Mem/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,OrganizationName,BankAcctNo,ArcSum,Secret,FocalPointId,StaticMsg")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.BusinessInfoes.Add(member);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FocalPointId = new SelectList(db.People, "id", "Fullname", member.FocalPointId);
            return View(member);
        }

        // GET: Mem/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = await db.BusinessInfoes.FindAsync(id);
            if (member == null)
            {
                return HttpNotFound();
            }
            ViewBag.FocalPointId = new SelectList(db.People, "id", "Fullname", member.FocalPointId);
            return View(member);
        }

        // POST: Mem/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,OrganizationName,BankAcctNo,ArcSum,Secret,FocalPointId,StaticMsg")] Member member)
        {
            if (ModelState.IsValid)
            {
                db.Entry(member).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FocalPointId = new SelectList(db.People, "id", "Fullname", member.FocalPointId);
            return View(member);
        }

        // GET: Mem/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Member member = await db.BusinessInfoes.FindAsync(id);
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
            Member member = await db.BusinessInfoes.FindAsync(id);
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
