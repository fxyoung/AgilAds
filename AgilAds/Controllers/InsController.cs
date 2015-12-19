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
    public class InsController : Controller
    {
        private AgilAdsDataContext db = new AgilAdsDataContext();

        // GET: Ins
        public async Task<ActionResult> Index()
        {
            var Institutuions = db.Institutions;
            return View(InstitutionListAllView.CreateListView(await Institutuions.ToListAsync()));
        }

        // GET: Ins/Details/5
        public async Task<ActionResult> Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Institution institution = await db.Institutions.FindAsync(id);
            if (institution == null)
            {
                return HttpNotFound();
            }
            return View(institution);
        }

        // GET: Ins/Create
        public ActionResult Create()
        {
            ViewBag.ContactMethod = new SelectList(Enum.GetNames(typeof(ContactInfo.contactMethod)));
            return View();
        }

        // POST: Ins/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Create([Bind(Include = "id,OrganizationName,BankAcctNo,FocalPointId,ArcSum,Secret,ParentId,MonthlyAdFee")] Institution institution)
        {
            if (ModelState.IsValid)
            {
                db.BusinessInfoes.Add(institution);
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }

            ViewBag.ContactMethod = new SelectList(Enum.GetNames(typeof(ContactInfo.contactMethod)));
            return View(institution);
        }

        // GET: Ins/Edit/5
        public async Task<ActionResult> Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Institution institution = await db.Institutions.FindAsync(id);
            if (institution == null)
            {
                return HttpNotFound();
            }
            ViewBag.ParentId = new SelectList(db.BusinessInfoes, "id", "OrganizationName", institution.ParentId);
            return View(institution);
        }

        // POST: Ins/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> Edit([Bind(Include = "id,OrganizationName,BankAcctNo,FocalPointId,ArcSum,Secret,ParentId,MonthlyAdFee")] Institution institution)
        {
            if (ModelState.IsValid)
            {
                db.Entry(institution).State = EntityState.Modified;
                await db.SaveChangesAsync();
                return RedirectToAction("Index");
            }
            ViewBag.ParentId = new SelectList(db.BusinessInfoes, "id", "OrganizationName", institution.ParentId);
            return View(institution);
        }

        // GET: Ins/Delete/5
        public async Task<ActionResult> Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Institution institution = await db.Institutions.FindAsync(id);
            if (institution == null)
            {
                return HttpNotFound();
            }
            return View(institution);
        }

        // POST: Ins/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public async Task<ActionResult> DeleteConfirmed(int id)
        {
            Institution institution = await db.Institutions.FindAsync(id);
            db.BusinessInfoes.Remove(institution);
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