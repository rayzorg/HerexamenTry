using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HerexamenTry.Shared.Domain
{
   public class Begeleider
    {
        
        public string Firstname { get; set; }

        [Required, Key]
        public string Email { get; set; }

        public virtual List<Jongere> Jongeren { get; set; }

        public virtual List<Reactie> Reacties { get; set; }

        public Begeleider()
        {

        }
        public Begeleider(string firstname,string email, List<Jongere> jongeren, List<Reactie> reacties)
        {
            Firstname = firstname;
            Email = email;
            Jongeren = jongeren;
            Reacties = reacties;
        }
    }
}
