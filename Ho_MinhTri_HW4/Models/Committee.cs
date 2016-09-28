using System;
using System.ComponentModel.DataAnnotations;

namespace Ho_MinhTri_HW4.Models
{
    public class Committee
    {
        //
        // A VERIFIER AVANT CONTROLLER
        //

        //ID
        [Display(Name = "Committee ID")]
        public Int32 CommitteeID { get; set; }

        //First Name
        [Required(ErrorMessage = "Committee name is required.")]
        [Display(Name = "Committee Name")]
        public String FirstName { get; set; }
    }
}