using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace planner.Models
{
    public class Wedding : BaseEntity
    {
        public int WeddingId {get;set;}
        public string Wedder1 { get; set; }
        public string Wedder2 {get;set;}
        public DateTime Date { get; set; }
        public string Address { get; set; }
        public int UserId { get; set; }
        public User User { get; set; }
        public List<Rsvp> Guests { get; set; }

        public Wedding(){
            Guests = new List<Rsvp>();
        }
    }
}