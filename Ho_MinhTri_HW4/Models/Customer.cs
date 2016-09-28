using System;
using System.ComponentModel.DataAnnotations;

namespace Ho_MinhTri_HW3.Models
{
    public class Customer
    {
        //ID
        [Display(Name = "Customer ID")]
        public Int32 CustomerID { get; set; }

        //First Name
        [Required(ErrorMessage = "First Name is required.")]
        [Display(Name = "First Name")]
        public String FirstName { get; set; }

        //Last Name
        [Required(ErrorMessage = "Last Name is required.")]
        [Display(Name = "Last Name")]
        public String LastName { get; set; }

        //Email
        [Required(ErrorMessage = "Email is required")]
        [DataType(DataType.EmailAddress, ErrorMessage = "Enter a valid email address.")]
        [Display(Name = "Email Address")]
        [EmailAddress(ErrorMessage = "Please enter a valid email address")]
        public String Email { get; set; }

        //Phone number
        [Required(ErrorMessage = "Phone number is required")]
        [DataType(DataType.PhoneNumber)]
        [Display(Name = "Phone Number")]
        [DisplayFormat(DataFormatString = "{0:###-###-####}", ApplyFormatInEditMode = true)]
        public String PhoneNumber { get; set; }

        //OK to text
        [Required(ErrorMessage = "This field is required")]
        [Display(Name = "Is it OK to text the member?")]
        public bool OKToText { get; set; }

        //Major
        public enum MajorList { Accounting, Business_Honors, Finance, International_Business, Management, MIS, Marketing, SCM, STM }
        [Required(ErrorMessage = "Major is required")]
        [Display(Name = "Major")]
        public MajorList Major { get; set; }
    }
}