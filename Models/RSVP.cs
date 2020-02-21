using System.ComponentModel.DataAnnotations;

namespace WeddingPlanner2.Models
{
    public class RSVP
    {
        [Key]
        public int RSVPId {set;get;}
        public int UserId {get;set;}
        public int WeddingId {get;set;}
        public User Guest {get;set;}
        public Wedding Wedding {get;set;}
    }
}