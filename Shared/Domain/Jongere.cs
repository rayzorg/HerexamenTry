﻿using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace HerexamenTry.Shared
{
   public class Jongere
    {
        public int Id { get; set; }

        
        public string Firstname { get; set; }
       
        public string Lastname { get; set; }
        
        public string Gender { get; set; }
       
       
        public DateTime Date { get; set; }
        
        public string Email { get; set; }
       
        public string Password { get; set; }

    }
}
