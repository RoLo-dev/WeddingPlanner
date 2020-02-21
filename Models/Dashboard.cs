using System;
using System.Collections.Generic;

namespace WeddingPlanner2.Models
{
    public class Dashboard
    {
        public User UserId {get;set;}
        public List<Wedding> WeddingList {get;set;}
        public RSVP isRSVP {get;set;}
    }
}