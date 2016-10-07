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
    public class CustomersController : Controller
    {
        private AppDbContext db = new AppDbContext();

        // GET: Customers
        public ActionResult Index()
        {
            return View(db.Customers.ToList());
        }

        // GET: Customers/Details/5
        public ActionResult Details(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // GET: Customers/Create
        public ActionResult Create()
        {
            //Add to ViewBag
            ViewBag.AllEvents = GetAllEvents();

            return View();
        }

        // POST: Customers/Create
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Create([Bind(Include = "CustomerID,FirstName,LastName,Email,PhoneNumber,OKToText,Major")] Customer customer)
        {
            if (ModelState.IsValid)
            {
                db.Customers.Add(customer);
                db.SaveChanges();
                return RedirectToAction("Index");
            }

            //Add to ViewBag
            ViewBag.AllEvents = GetAllEvents(customer);
            return View(customer);
        }

        // GET: Customers/Edit/5
        public ActionResult Edit(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }

            //Add to ViewBag
            ViewBag.AllEvents = GetAllEvents(customer);
            return View(customer);
        }

        // POST: Customers/Edit/5
        // To protect from overposting attacks, please enable the specific properties you want to bind to, for 
        // more details see http://go.microsoft.com/fwlink/?LinkId=317598.
        [HttpPost]
        [ValidateAntiForgeryToken]
        public ActionResult Edit([Bind(Include = "CustomerID,FirstName,LastName,Email,PhoneNumber,OKToText,Major")] Customer customer, int[] SelectedEvents)
        {
            if (ModelState.IsValid)
            {
                //Find associated customer
                Customer customerToChange = db.Customers.Find(customer.CustomerID);

                //Change event
                //Remove any existing event
                customerToChange.EventAttending.Clear();

                //Add members if needed
                if (customerToChange != null)
                {
                    foreach (int eventID in SelectedEvents)
                    {
                        Event eventToAdd = db.Events.Find(eventID);
                        customerToChange.EventAttending.Add(eventToAdd);
                    }
                }

                //Update the rest of the fields
                customerToChange.FirstName = customer.FirstName;
                customerToChange.LastName = customer.LastName;
                customerToChange.Email = customer.Email;
                customerToChange.PhoneNumber = customer.PhoneNumber;
                customerToChange.OKToText = customer.OKToText;
                customerToChange.Major = customer.Major;

                db.Entry(customerToChange).State = EntityState.Modified;
                db.SaveChanges();

                return RedirectToAction("Index");
            }

            //Add to ViewBag
            ViewBag.AllEvents = GetAllEvents(customer);
            return View(customer);
        }

        // GET: Customers/Delete/5
        public ActionResult Delete(int? id)
        {
            if (id == null)
            {
                return new HttpStatusCodeResult(HttpStatusCode.BadRequest);
            }
            Customer customer = db.Customers.Find(id);
            if (customer == null)
            {
                return HttpNotFound();
            }
            return View(customer);
        }

        // POST: Customers/Delete/5
        [HttpPost, ActionName("Delete")]
        [ValidateAntiForgeryToken]
        public ActionResult DeleteConfirmed(int id)
        {
            Customer customer = db.Customers.Find(id);
            db.Customers.Remove(customer);
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
        public MultiSelectList GetAllEvents()
        {
            //Populate list of members
            var query = from e in db.Events
                        orderby e.EventDate
                        select e;

            //create list and execute query
            List<Event> allEvents = query.ToList();

            SelectList allMemberList = new SelectList(allEvents, "EventTitle", "EventDate");

            return allMemberList;
        }

        // SelectList Events
        public MultiSelectList GetAllEvents(Customer @customer)
        {
            //Populate list of events
            var query = from e in db.Events
                        orderby e.EventDate
                        select e;

            //create list and execute query
            List<Event> allEvents = query.ToList();

            //Create list of selected events
            List<Int32> SelectedEvents = new List<Int32>();

            //Loop through list of events and add EventID
            foreach (Event e in @customer.EventAttending) //////////////////////
            {
                SelectedEvents.Add(e.EventID);
            }

            MultiSelectList allMemberList = new MultiSelectList(allEvents, "EventID", "EventTitle", SelectedEvents);

            return allMemberList;
        }
    }
}
