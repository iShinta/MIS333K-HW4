using System;
using System.Collections.Generic;
using System.Data;
using System.Data.Entity;
using System.Linq;
using System.Net;
using System.Web;
using System.Web.Mvc;
using Ho_MinhTri_HW4.DAL;
using Ho_MinhTri_HW4.Models;

namespace Ho_MinhTri_HW4.Controllers
{
    public class CommitteesController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Committees
        public ActionResult Index()
        {
            return View(db.Committees.ToList());
        }

        // GET: Committees/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Committee committee = db.Committees.Find(id);
            if (committee == null)
            {
                return HttpNotFound();
            }
            return View(committee);
        }

        // GET: Committees/Create
        public ActionResult Create()
        {
            return View();
        }

        // POST: Committees/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CommitteeID,CommitteeName")] Committee committee)
        {
            if (ModelState.IsValid)
            {
                db.Committees.Add(committee);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AllCommittees = GetAllEvents(committee);
            return View(committee);
        }

        // GET: Committees/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Committee committee = db.Committees.Find(id);
            if (committee == null)
            {
                return HttpNotFound();
            }

            //Add to ViewBag
            ViewBag.AllCommittees = GetAllEvents(committee);
            return View(committee);
        }

        // POST: Committees/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CommitteeID,CommitteeName")] Committee committee)
        {
            if (ModelState.IsValid)
            {
                db.Entry(committee).State = EntityState.Modified;
                db.SaveChanges();
                return RedirectToAction("Index");
            }
            //Add to ViewBag
            ViewBag.AllCommittees = GetAllEvents(committee);
            return View(committee);
        }

        // GET: Committees/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Committee committee = db.Committees.Find(id);
            if (committee == null)
            {
                return HttpNotFound();
            }
            return View(committee);
        }

        // POST: Committees/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Committee committee = db.Committees.Find(id);
            db.Committees.Remove(committee);
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

        // SelectList Events
        public MultiSelectList GetAllEvents(Committee @committee)
        {
            //Populate list of members
            var query = from e in db.Events
                        orderby e.EventDate
                        select e;

            //create list and execute query
            List<Event> allEvents = query.ToList();

            //Create list of selected members
            List<Int32> SelectedEvents = new List<Int32>();

            //Loop through list of members and add MemberId
            foreach (Event m in @committee.EventsSponsored)
            {
                SelectedEvents.Add(m.EventID);
            }

            MultiSelectList allMemberList = new MultiSelectList(allEvents, "EventTitle", "EventDate", SelectedEvents);

            return allMemberList;
        }
    }
}
