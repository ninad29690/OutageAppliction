using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using ApplicationOutage;

namespace ApplicationOutage.Controllers
{
    public class OutagesController : Controller
    {
        private ApplicationOutageEntities db = new ApplicationOutageEntities();

        // GET: Outages
        public ActionResult Index()
        {
            var outages = db.Outages.Include(o => o.Application);
            return View(outages.ToList());
        }

        // GET: Outages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outage outage = db.Outages.Find(id);
            if (outage == null)
            {
                return HttpNotFound();
            }
            return View(outage);
        }

        // GET: Outages/Create
        public ActionResult Create()
        {
            ViewBag.ApplicationID = new SelectList(db.Applications, "ID", "ApplicationName");
            return View();
        }

        // POST: Outages/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "ID,ApplicationID,StartDate,EndDate,IncidentNumber,Impact,Description,Component")] Outage outage)
        {
            if (ModelState.IsValid)
            {
                db.Outages.Add(outage);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.ApplicationID = new SelectList(db.Applications, "ID", "ApplicationName", outage.ApplicationID);
            return View(outage);
        }

        // GET: Outages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outage outage = db.Outages.Find(id);
            if (outage == null)
            {
                return HttpNotFound();
            }
            ViewBag.ApplicationID = new SelectList(db.Applications, "ID", "ApplicationName", outage.ApplicationID);
            return View(outage);
        }

        // POST: Outages/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "ID,ApplicationID,StartDate,EndDate,IncidentNumber,Impact,Description,Component")] Outage outage)
        {
            if (ModelState.IsValid)
            {
                db.Entry(outage).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            ViewBag.ApplicationID = new SelectList(db.Applications, "ID", "ApplicationName", outage.ApplicationID);
            return View(outage);
        }

        // GET: Outages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Outage outage = db.Outages.Find(id);
            if (outage == null)
            {
                return HttpNotFound();
            }
            return View(outage);
        }

        // POST: Outages/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Outage outage = db.Outages.Find(id);
            db.Outages.Remove(outage);
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
