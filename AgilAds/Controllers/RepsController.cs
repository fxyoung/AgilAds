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
    public class RepsController : Controller
    {
        private AgilAdsDataContext db = new AgilAdsDataContext();

        // GET: Reps
        public async Task<ActionResult> Index()
        {
            var businessInfoes = db.Rep.Include(r => r.FocalPoint);
            return View(await businessInfoes.ToListAsync());
        }

        // GET: Reps/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rep rep = await db.Rep.FindAsync(id);
            if (rep == null)
            {
                return HttpNotFound();
            }
            return View(rep);
        }

        // GET: Reps/Create
        public ActionResult Create()
        {
            ViewBag.FocalPointId = new SelectList(db.People, "id", "Fullname");
            return View();
        }

        // POST: Reps/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,OrganizationName,BankAcctNo,Modified,ModifiedBy,ArcSum,Secret,Region,FocalPointId,Fee,TaxRate")] Rep rep)
        {
            if (ModelState.IsValid)
            {
                db.Rep.Add(rep);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.FocalPointId = new SelectList(db.People, "id", "Fullname", rep.FocalPointId);
            return View(rep);
        }

        // GET: Reps/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rep rep = await db.Rep.FindAsync(id);
            if (rep == null)
            {
                return HttpNotFound();
            }
            ViewBag.FocalPointId = new SelectList(db.People, "id", "Fullname", rep.FocalPointId);
            return View(rep);
        }

        // POST: Reps/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,OrganizationName,BankAcctNo,Modified,ModifiedBy,ArcSum,Secret,Region,FocalPointId,Fee,TaxRate")] Rep rep)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rep).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.FocalPointId = new SelectList(db.People, "id", "Fullname", rep.FocalPointId);
            return View(rep);
        }

        // GET: Reps/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rep rep = await db.Rep.FindAsync(id);
            if (rep == null)
            {
                return HttpNotFound();
            }
            return View(rep);
        }

        // POST: Reps/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Rep rep = await db.Rep.FindAsync(id);
            db.Rep.Remove(rep);
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
