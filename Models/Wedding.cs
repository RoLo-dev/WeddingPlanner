using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner2.Models
{
    public class Wedding
    {
        [Key]
        public int WeddingId {get;set;}
        public int UserId {get;set;}
        public User Creator {get;set;}

        [Required(ErrorMessage="Please enter a name")]
        [Display(Name="Wedder one:")]
        public string WedderOne {get;set;}

        [Required(ErrorMessage="Please enter a name")]
        [Display(Name="Wedder two:")]
        public string WedderTwo {get;set;}

        [FutureDate]
        [Required(ErrorMessage="Date should be a future date")]
        [Display(Name="Date:")]
        [DataType(DataType.Date)]
        public DateTime Date {get;set;}

        [Required(ErrorMessage="Please enter an address")]
        [Display(Name="Address:")]
        public string Address {get;set;}

        public List<RSVP> RSVPs {get;set;}

        public DateTime CreatedAt {get;set;} = DateTime.Now;
        public DateTime UpdateAt {get;set;} = DateTime.Now;

    }
}