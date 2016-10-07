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
    public class EventsController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Events
        public ActionResult Index()
        {
            return View(db.Events.ToList());
        }

        // GET: Events/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // GET: Events/Create
        public ActionResult Create()
        {
            //Add to ViewBag
            ViewBag.AllCommittees = GetAllCommittees();

            return View();
        }

        // POST: Events/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "EventID,EventTitle,EventDate,EventLocation,CustomersOnly")] Event @event, Int32 CommitteeID)
        {
            //Find selected committee
            Committee SelectedCommittee = db.Committees.Find(CommitteeID);

            //Associate Committee with Event
            @event.SponsoringCommittee = SelectedCommittee;

            if (ModelState.IsValid)
            {
                db.Events.Add(@event);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            ViewBag.AllCommittees = GetAllCommittees(@event);
            return View(@event);
        }

        // GET: Events/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }

            //Add to ViewBag
            ViewBag.AllCommittees = GetAllCommittees(@event);
            ViewBag.AllMembers = GetAllMembers(@event);

            return View(@event);
        }

        // POST: Events/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "EventID,EventTitle,EventDate,EventLocation,CustomersOnly")] Event @event, Int32 CommitteeID, int[] SelectedMembers)
        {
            if (ModelState.IsValid)
            {
                //Find associated event
                Event eventToChange = db.Events.Find(@event.EventID);

                //Change committee if necessary
                if(eventToChange.SponsoringCommittee.CommitteeID != CommitteeID)
                {
                    //Find committee
                    Committee SelectedCommittee = db.Committees.Find(CommitteeID);

                    //Update the committee
                    eventToChange.SponsoringCommittee = SelectedCommittee;
                }

                //Change members
                //Remove any existing members
                eventToChange.Customers.Clear();

                //Add members if needed
                if(SelectedMembers != null)
                {
                    foreach (int customerID in SelectedMembers)
                    {
                        Customer memberToAdd = db.Customers.Find(customerID);
                        eventToChange.Customers.Add(memberToAdd);
                    }
                }

                //Update the rest of the fields
                eventToChange.EventTitle = @event.EventTitle;
                eventToChange.EventDate = @event.EventDate;
                eventToChange.EventLocation = @event.EventLocation;
                eventToChange.CustomersOnly = @event.CustomersOnly;

                db.Entry(eventToChange).State = EntityState.Modified;
                db.SaveChanges();

                ViewBag.AllCommittees = GetAllCommittees(@event);
                ViewBag.AllMembers = GetAllMembers(@event);

                return RedirectToAction("Index");
            }

            //Repopulate lists
            //Add to ViewBag
            ViewBag.AllCommittees = GetAllCommittees(@event);
            ViewBag.AllMembers = GetAllMembers(@event);

            return View(@event);
        }

        // GET: Events/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Event @event = db.Events.Find(id);
            if (@event == null)
            {
                return HttpNotFound();
            }
            return View(@event);
        }

        // POST: Events/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Event @event = db.Events.Find(id);
            db.Events.Remove(@event);
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

        // SelectList Committees
        //COMMITTEE ALREADY CHOSEN 
        public SelectList GetAllCommittees(Event @event)
        {
            //Populate list of committees
            var query = from c in db.Committees
                        orderby c.CommitteeName
                        select c;

            //create list and execute query
            List<Committee> allCommittees = query.ToList();

            //convert to select list
            SelectList allCommitteesList = new SelectList(allCommittees, "CommitteeID", "CommitteeName", @event.SponsoringCommittee.CommitteeID);

            return allCommitteesList;
        }

        // SelectList Committees
        public SelectList GetAllCommittees()
        {
            //Populate list of committees
            var query = from c in db.Committees
                        orderby c.CommitteeName
                        select c;

            //create list and execute query
            List<Committee> allCommittees = query.ToList();

            //convert to select list
            SelectList allCommitteesList = new SelectList(allCommittees, "CommitteeID", "CommitteeName");

            return allCommitteesList;
        }

        // SelectList Committees
        public MultiSelectList GetAllMembers(Event @event)
        {
            //Populate list of members
            var query = from m in db.Customers
                        orderby m.Email
                        select m;

            //create list and execute query
            List<Customer> allMembers = query.ToList();

            //Create list of selected members
            List<Int32> SelectedMembers = new List<Int32>();

            //Loop through list of members and add MemberId
            foreach (Customer m in @event.Customers)
            {
                SelectedMembers.Add(m.CustomerID);
            }

            MultiSelectList allMemberList = new MultiSelectList(allMembers, "CustomerID", "Email", SelectedMembers);

            return allMemberList;
        }
    }
}
