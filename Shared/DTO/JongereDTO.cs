using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HerexamenTry.Shared.DTO
{
   public class JongereDTO
    {

        [Required(ErrorMessage = "Mag niet leeg zijn.")]
       
        public string Firstname { get; set; }
        [Required(ErrorMessage = "Mag niet leeg zijn.")]
        public string Username { get; set; }
        [Required(ErrorMessage = "Mag niet leeg zijn.")]
        public string Lastname { get; set; }
       [Required(ErrorMessage = "Mag niet leeg zijn.")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Mag niet leeg zijn.")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Mag niet leeg zijn.")]
        [DataType(DataType.EmailAddress)]
        [EmailAddress(ErrorMessage = "Moet een geldig emailadres zijn")]
        [RegularExpression("^[a-z0-9_\\+-]+(\\.[a-z0-9_\\+-]+)*@[a-z0-9-]+(\\.[a-z0-9]+)*\\.([a-z]{2,4})$", ErrorMessage = "Foute formaat.")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mag niet leeg zijn.")]
        [DataType(DataType.Password)]
        [RegularExpression("(?=.*[0-9])(?=.*[a-z])(?=.*[A-Z])(?=.*[\\W]).{6,}", ErrorMessage = "Moet een grote letter ,kleiner letter, nummer en een speciale teken bevatten.")]
        public string Password { get; set; }

    }
}
