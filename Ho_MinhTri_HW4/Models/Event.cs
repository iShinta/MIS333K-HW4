using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ho_MinhTri_HW4.Models
{
    public class Event
    {
        //
        // STATUS: READY FOR CONTROLLER
        //

        //SCALAR PROPERTIES
        //ID - OK
        [Display(Name = "Event ID")]
        public Int32 EventID { get; set; }

        //Event Title - OK
        [Required(ErrorMessage = "Event title is required.")]
        [Display(Name = "Event Title")]
        public String EventTitle { get; set; }

        //Date - OK
        [Required(ErrorMessage = "Event date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Enter a date.")]
        [DisplayFormat(DataFormatString = "{0:yyyy-MM-dd}", ApplyFormatInEditMode = true)]
        [Display(Name = "Date")]
        public DateTime EventDate { get; set; }

        //Location - OK
        [Required(ErrorMessage = "Event location is required")]
        [Display(Name = "Event Location")]
        public String EventLocation { get; set; }

        //Members only - OK
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Members only?")]
        public Boolean CustomersOnly { get; set; }

        //NAVIGATION PROPERTIES
        [Display (Name = "Sponsoring Committee")]
        public virtual Committee SponsoringCommittee { get; set; }
        public virtual List<Customer> Members { get; set; }
    }
}