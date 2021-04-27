using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace ConferenceApp.Models
{
    public enum ConferenceType
    {
        [Display(Name ="Warsztaty")]
        Workshop,
        [Display(Name = "Wykłady")]
        Lecture,
        [Display(Name = "Zdalnie")]
        Remote
    }
    public class ConferenceUser
    {
        [Display(Name = "Imię")]
        [Required(ErrorMessage = "First Name Required")]
        public string FirstName { get; set; }
        [Display(Name ="Nazwisko")]
        [Required(ErrorMessage = "Last Name Required")]
        public string LastName { get; set; }
        [Display(Name ="E-mail")]
        [Required(ErrorMessage = "Email Required")]
        public string Email { get; set; }
        [Display(Name ="Zdjęcie")]
        [Required(ErrorMessage = "File  Required")]
        public IFormFile File{ get; set; }
        [Display(Name ="Zdjęcie 2")]
        [Required(ErrorMessage = "File  Required")]
        public IFormFile FileSec{ get; set; }
        [Display(Name = "Typ konfencji")]
        [Required(ErrorMessage = "Conference Type Required")]
        public Nullable<ConferenceType> ConferenceType { get; set; }//nullable pozowli "" wartosci dawac
    }
}
