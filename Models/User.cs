using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;
using System.Linq;

namespace GlobalATM.Models
{
    public class User
    {
        [Key]
        public int UserId {get;set;}

        [Display(Name="First Name")]
        [MinLength(2, ErrorMessage ="First Name must be at least two characters")]
        [Required]
        public string FirstName {get;set;}

        [Required]
        [MinLength(2, ErrorMessage ="First Name must be at least two characters")]
        [Display(Name="Last Name")]
        public string LastName {get;set;}

        [Required]
        [RegularExpression(@"^[\w!#$%&'+-/=?^_`{|}~]+(.[\w!#$%&'+-/=?^_`{|}~]+)*" + "@" + @"((([-\w]+.)+[a-zA-Z]{2,4})|(([0-9]{1,3}.){3}[0-9]{1,3}))\z")]
        [EmailAddress]
        public string Email {get;set;}

        [Required]
        [MinLength(4, ErrorMessage ="Please enter at least 4 characters")]
        [DataType(DataType.Password)]
        public string Pin {get;set;}

        
        [Required]
        [MinAge(18, ErrorMessage ="You must be at least 18 years old")]
        public DateTime Birthday {get; set;}

        public DateTime CreatedAt { get; set; } = DateTime.Now;
        public DateTime UpdatedAt { get; set; } = DateTime.Now;

        public List<Account> Accounts {get; set;} = new List<Account>();


        [Compare("Pin")]
        [DataType(DataType.Password)]
        [NotMapped]
        public string Confirm {get;set;}

        [NotMapped]
        [Required]
        public string AccountType {get; set;}

        [NotMapped]
        public string CardNumber {get; set;}

        [NotMapped]
        public string AccountNumber {get; set;}

        public class MinAge : ValidationAttribute
        {
            private int _Limit;
            public MinAge(int Limit)
            {
                this._Limit = Limit;
            }
            protected override ValidationResult IsValid(object value, ValidationContext validationContext) 
            {
                DateTime bday = DateTime.Parse(value.ToString());
                DateTime today = DateTime.Today;
                int age = today.Year - bday.Year;
                if (bday > today.AddYears(-age))
                {
                    age--; 
                }
                if (age < _Limit)
                {
                    var result = new ValidationResult("You must be 18 years or older");
                    return result; 
                }            
                return null;
            }
        }
    }
}