using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Ho_MinhTri_HW4.Models
{
    public class Committee
    {
        //
        // STATUS: READY FOR CONTROLLER
        //

        //SCALAR PROPERTIES
        //ID
        [Display(Name = "Committee ID")]
        public Int32 CommitteeID { get; set; }

        //Committee Name
        [Required(ErrorMessage = "Committee name is required.")]
        [Display(Name = "Committee Name")]
        public String CommitteeName { get; set; }

        //NAVIGATIONAL PROPERTIES
        [Display (Name = "Comprising Members")]
        public virtual List<Customer> CustomersList { get; set; }
        public virtual List<Event> EventsSponsored { get; set; }
    }
}