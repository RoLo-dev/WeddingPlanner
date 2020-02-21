using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace WeddingPlanner2.Models
{
    public class User
    {
        [Key]
        public int UserId {get;set;}

        [Required(ErrorMessage="Please enter your name")]
        [Display(Name="First name:")]
        [MinLength(2, ErrorMessage="Name should be more than 2 characters")]
        public string FirstName {get;set;}

        [Required(ErrorMessage="Please enter your last name")]
        [Display(Name="Last name:")]
        [MinLength(2, ErrorMessage="Last name should be more than 2 characters")]
        public string LastName {get;set;}

        [Required(ErrorMessage="Please enter an email")]
        [Display(Name="Email:")]
        [EmailAddress]
        public string Email {get;set;}

        [Required(ErrorMessage="Please create a password")]
        [Display(Name="Password:")]
        [MinLength(5, ErrorMessage="Password should be more than 5 characters")]
        [DataType(DataType.Password)]
        public string Password {get;set;}

        [NotMapped]
        [Required(ErrorMessage="Please confirm your password")]
        [Display(Name="Confirm password:")]
        [DataType(DataType.Password)]
        [MinLength(5, ErrorMessage="Password should be more than 5 characters")]
        [Compare("Password", ErrorMessage="Passwords don't match")]
        public string Confirm {get;set;}
        public List<Wedding> CreatedWeddings {get;set;}
        public List<RSVP> CreatedRSVP {get;set;}

        [Required]
        public DateTime CreatedAt {get;set;} = DateTime.Now;

        [Required]
        public DateTime UpdatedAt {get;set;} = DateTime.Now;
    }
}