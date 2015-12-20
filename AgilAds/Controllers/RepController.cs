﻿using System;
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
    public class RepController : Controller
    {
        private AgilAdsDataContext db = new AgilAdsDataContext();

        // GET: Rep
        public async Task<ActionResult> Index()
        {
            var reps = db.Reps;
            return View(RepListAllView.CreateListView(await reps.ToListAsync()));
        }

        // GET: Rep/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rep rep = await db.Reps.FindAsync(id);
            if (rep == null)
            {
                return HttpNotFound();
            }
            return View(rep);
        }

        // GET: Rep/Create
        public ActionResult Create()
        {
            ViewBag.ContactMethod = new SelectList(Enum.GetNames(typeof(ContactInfo.contactMethod)));
            return View();
        }

        // POST: Rep/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id," + Helpers.Constants.CreateViewCommonBindSpec + ",Region,Fee,TaxRate")] RepCreateView rep)
        {
            if (ModelState.IsValid)
            {
                var nuRep = new Rep(rep);
                db.BusinessInfoes.Add(nuRep);
                await db.SaveChangesAsync();
                if (nuRep.Team.Count() == 1)
                {
                    nuRep.FocalPointId = nuRep.Team.First().id;
                    await db.SaveChangesAsync();
                }
                return RedirectToAction("Index");
            }

            ViewBag.ContactMethod = new SelectList(Enum.GetNames(typeof(ContactInfo.contactMethod)));
            return View(rep);
        }

        // GET: Rep/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rep rep = await db.Reps.FindAsync(id);
            if (rep == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentId = new SelectList(db.BusinessInfoes, "id", "OrganizationName", rep.ParentId);
            return View(rep);
        }

        // POST: Rep/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,OrganizationName,BankAcctNo,FocalPointId,ArcSum,Secret,ParentId,Region,Fee,TaxRate")] Rep rep)
        {
            if (ModelState.IsValid)
            {
                db.Entry(rep).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ParentId = new SelectList(db.BusinessInfoes, "id", "OrganizationName", rep.ParentId);
            return View(rep);
        }

        // GET: Rep/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Rep rep = await db.Reps.FindAsync(id);
            if (rep == null)
            {
                return HttpNotFound();
            }
            return View(rep);
        }

        // POST: Rep/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Rep rep = await db.Reps.FindAsync(id);
            db.BusinessInfoes.Remove(rep);
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
