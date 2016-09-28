using System;
using System.ComponentModel.DataAnnotations;

namespace Ho_MinhTri_HW4.Models
{
    public class Event
    {
        //
        // STATUS: A VERIFIER AVANT CONTROLLER
        //

        //ID
        [Display(Name = "Event ID")]
        public Int32 EventID { get; set; }

        //Event Title
        [Required(ErrorMessage = "Event title is required.")]
        [Display(Name = "Event Title")]
        public String EventTitle { get; set; }

        //Date - A FAIRE
        [Required(ErrorMessage = "Event date is required.")]
        [DataType(DataType.Date, ErrorMessage = "Enter a date.")]
        [Display(Name = "Last Name")]
        public String EventDate { get; set; }

        //Location
        [Required(ErrorMessage = "Event location is required")]
        [Display(Name = "Event Location")]
        public String EventLocation { get; set; }

        //Members only
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Members only?")]
        public bool MembersOnly { get; set; }
    }
}