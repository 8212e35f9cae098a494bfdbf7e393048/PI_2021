using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Threading.Tasks;

namespace MVCMissingPersonAppValid.Models
{
    public class PersonModel
    {
        [Required(ErrorMessage = "Imię jest wymagane")]
        public string FirstName { get; set; }
        [Required(ErrorMessage = "Nazwisko jest wymagane")]
        public string LastName { get; set; }
        [Required(ErrorMessage = "Płeć jest wymagana")]
        public char Gender { get; set; }
        [Required(ErrorMessage = "Wiek jest wymagany")]
        public int Age { get; set; }
        [Required(ErrorMessage = "Opis jest wymagany")]
        public string Description { get; set; }
        [Required(ErrorMessage = "Zdjęcie jest wymagane")]
        public IFormFile Picture { get; set; }
        [Required(ErrorMessage = "Miasto jest wymagane")]
        public string City { get; set; }

    }
}
