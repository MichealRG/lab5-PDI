using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Models
{
    public enum ConferenceType
    {
        [Display(Name = "Warsztaty")]
        Workshop,

        [Display(Name = "Wykłady")]
        Lecture,

        [Display(Name = "Zdalnie")]
        Remote
        //....
    }

    public class ConferenceUser
    {
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string Color { get; set; }

        public ConferenceType? ConferenceType { get; set; }

    }
}
