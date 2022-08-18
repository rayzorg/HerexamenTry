using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HerexamenTry.Shared.Domain
{
    public class Reactie
    {

        [Required, Key, DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id { get; set; }
        public string Text { get; set; }
        public virtual Post Post { get; set; }
        public virtual Begeleider Begeleider { get; set; }

        public Reactie()
        {
        }

        public Reactie(int id, string text, Post post, Begeleider begeleider)
        {
            Id = id;
            Text = text;
            Post = post;
            Begeleider = begeleider;
        }
    }
}
