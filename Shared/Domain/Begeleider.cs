using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HerexamenTry.Shared.Domain
{
   public class Begeleider
    {

       
        public int Id { get; set; }


        public string Firstname { get; set; }

        public string Lastname { get; set; }

        public string Gender { get; set; }


        public DateTime Date { get; set; }

        public string Email { get; set; }

        public string Password { get; set; }

        public List<Jongere> Jongeren { get; set; }

        public List<Reactie> Reacties { get; set; }
    }
}
