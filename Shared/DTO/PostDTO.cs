using HerexamenTry.Shared.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HerexamenTry.Shared.DTO
{
  public  class PostDTO { 


        public Jongere Jongere { get; set; }
        [Required(ErrorMessage = "Mag niet leeg zijn.")]
        public string Text { get; set; }
         public List<Reactie> Reacties { get; set; }
    
    }
}
