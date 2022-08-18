using HerexamenTry.Shared.Domain;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HerexamenTry.Shared
{
   public class Jongere
    {


        [Required, Key]
        public string Username { get; set; }
        public string Firstname { get; set; }
       
        public string Lastname { get; set; }
        
        public string Gender { get; set; }
       
       
        public DateTime Date { get; set; }
        
        public string Email { get; set; }
       
        public string Password { get; set; }

        public virtual Begeleider Begeleider { get; set; }
       
        public virtual List<Post> Posts { get; set; }

        public Jongere()
        {
        }

        public Jongere( string username, string firstname, string lastname
            , string gender, DateTime date, string email, string password
            , Begeleider begeleider, List<Post> posts)
        {
         
            Username = username;
            Firstname = firstname;
            Lastname = lastname;
            Gender = gender;
            Date = date;
            Email = email;
            Password = password;
            Begeleider = begeleider;
            Posts = posts;
        }
    }
}
