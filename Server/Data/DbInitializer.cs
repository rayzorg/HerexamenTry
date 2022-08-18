using HerexamenTry.Shared;
using HerexamenTry.Shared.Domain;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace HerexamenTry.Server.Data
{
    public static class DbInitializer
    {

        public static void Initialize(HerexamenContext context)
        {
            context.Database.EnsureCreated();
            if (context.Begeleiders.Any())
            {
                return;   // DB has been seeded
            }

            var jongeren = new List<Jongere>();
            var reacties = new List<Reactie>();

            var jongeren1 = new List<Jongere>();
            var reacties1 = new List<Reactie>();
            var begeleiders = new Begeleider[]
            {
                new Begeleider{Firstname="Rayan",Email="wilm@live.be",Jongeren=jongeren,Reacties=reacties},
                 new Begeleider{Firstname="wilm",Email="wilm1@live.be",Jongeren=jongeren1,Reacties=reacties1}

            };
          
            foreach (Begeleider s in begeleiders)
            {
                context.Begeleiders.Add(s);
            }
            context.SaveChanges();

            
        }
    }
}
