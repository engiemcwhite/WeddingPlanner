using System;
using System.ComponentModel.DataAnnotations;
using System.Collections.Generic;

namespace planner.Models
{
    public abstract class BaseEntity {}
    public class User : BaseEntity
    {
        public int UserId {get;set;}
        public string FirstName { get; set; }
        public string LastName {get;set;}
        public string Email { get; set; }
        public string Password { get; set; }
        public string PasswordConfirmation { get; set; }
        public List<Wedding> Weddings { get; set; }

        public List<Rsvp> Rsvps { get; set; }

        public User(){
            Weddings = new List<Wedding>();
            Rsvps = new List<Rsvp>();
        }
    }

    
}