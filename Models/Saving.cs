using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace GlobalATM.Models
{
    public class Saving : Account
    {
        public double InterestRate {get; set;}

        public string RandomDigits(int length)
            {
                var random = new Random();
                string s = string.Empty;
                for (int i = 0; i < length; i++)
                    s = String.Concat(s, random.Next(10).ToString());
                return s;
            }
    }
}    
