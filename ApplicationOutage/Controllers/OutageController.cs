using ApplicationOutage.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Web;
using System.Web.Mvc;
using System.Net;
using System.Data.Entity;



namespace ApplicationOutage.Controllers
{
    public class OutageController : Controller
    {
        // GET: Outages/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OutageManager outageManager = new OutageManager();
            Outage outage = outageManager.GetOutage(id);
            if (outage == null)
            {
                return HttpNotFound();
            }
            return View(outage);
        }

        // GET: Outage
        public ActionResult Index()
        {
            List<Outage> outage;
            ApplicationManager applicationManager = new ApplicationManager();
            applicationManager.GetApplicationList();
            ViewBag.Applications = applicationManager.GetApplicationList();
            ViewBag.Components = Components.GetComponents();
            using (ApplicationOutageEntities entities = new ApplicationOutageEntities())
            {
                outage = entities.Outages.Include("Application").ToList();
            }
            return View(outage);
        }

        public ActionResult Add()
        {
            ApplicationOutageEntities entities = new ApplicationOutageEntities();
            ViewBag.Applications = entities.Applications.Select(x => new SelectListItem { Text = x.ApplicationName, Value = x.ID.ToString() });
            ViewBag.Components = Components.GetComponents();
            return View();
        }

        [HttpPost]
        public ActionResult Save(Outage outage) {

            if (ModelState.IsValid)
            {
                OutageManager outageManager = new OutageManager();

                if (outageManager.AddOutage(outage))
                {
                    return RedirectToAction("Index");
                }
            }
            return View(outage);
            
        }

        public ActionResult TotalAvailability()
        {
            List<SelectListItem> years = new List<SelectListItem>();
            int currentYear = DateTime.Now.Year;
            for (int i = -10; i <=10; i++)
            {
                years.Add(new SelectListItem(){Value=(currentYear + i).ToString(), Text= (currentYear + i).ToString() });
            }
            ViewBag.Years = years;
            
            ViewBag.Months= Enum.GetValues(typeof(Months)).OfType<Enum>().Select(x => new SelectListItem { Text = Enum.GetName(typeof(Months), x), Value = (Convert.ToInt32(x).ToString()) });

            return View();
        }

        [HttpPost]
        public JsonResult TotalAvailability(int Year,int Month)
        {
            double TotalOutage = 0;
            DateTime startDate = new DateTime(Year, Month, 1, 00, 00, 00);
            DateTime endDate = new DateTime(Year, Month, DateTime.DaysInMonth(Year, Month), 23, 59, 00);
            using (ApplicationOutageEntities entities = new ApplicationOutageEntities())
            {
               var outageList = entities.Outages.Where(x => x.StartDate >= startDate && x.EndDate <= endDate).ToList();
                if(outageList!=null && outageList.Any())
                {
                    
                    foreach (var outage in outageList)
                    {
                         TotalOutage += (outage.EndDate - outage.StartDate).TotalMinutes;
                    }
                }
            }
                return Json(new { outage = TotalOutage });
        }

        // GET: Outages/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OutageManager outageManager = new OutageManager();
            Outage outage = outageManager.GetOutage(id);
            if (outage == null)
            {
                return HttpNotFound();
            }
            ApplicationManager appManager = new ApplicationManager();
            ViewBag.ApplicationID = new SelectList(appManager.GetApplicationList(), "ID", "ApplicationName", outage.ApplicationID);
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
                OutageManager outageManager = new OutageManager();

                if (outageManager.EditOutage(outage))
                {
                    return RedirectToAction("Index");
                }

               
                return RedirectToAction("Index");
            }
           // ViewBag.ApplicationID = new SelectList(db.Applications, "ID", "ApplicationName", outage.ApplicationID);
            return View(outage);
        }

        // GET: Outages/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            OutageManager outageManager = new OutageManager();
            Outage outage = outageManager.GetOutage(id);
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
            OutageManager outageManager = new OutageManager();
            outageManager.DeleteOutage(id);
            return RedirectToAction("Index");
        }

    }
}