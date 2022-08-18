using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HerexamenTry.Shared.Domain
{
    public class Post
    {
        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public virtual Jongere Jongere { get; set; }
       
        public string Text { get; set; }
        public virtual List<Reactie> Reacties { get; set; }

        public Post()
        {

        }

        public Post(int id, Jongere jongere, string text, List<Reactie> reacties)
        {
            Id = id;
            Jongere = jongere;
            Text = text;
            Reacties = reacties;
        }
    }

    
}
