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
       
        public string Lastname { get; set; }
       [Required(ErrorMessage = "Mag niet leeg zijn.")]
        public string Gender { get; set; }
        [Required(ErrorMessage = "Mag niet leeg zijn.")]
        [DataType(DataType.Date)]
        public DateTime Date { get; set; }
        [Required(ErrorMessage = "Mag niet leeg zijn.")]
        [EmailAddress(ErrorMessage = "Moet een geldig emailadres zijn")]
        public string Email { get; set; }
        [Required(ErrorMessage = "Mag niet leeg zijn.")]
        public string Password { get; set; }

    }
}
