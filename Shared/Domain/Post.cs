using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HerexamenTry.Shared.Domain
{
    public class Post
    {

        public Jongere Jongere { get; set; }
       
        public string Text { get; set; }
        public List<Reactie> Reacties { get; set; }
    }
}
