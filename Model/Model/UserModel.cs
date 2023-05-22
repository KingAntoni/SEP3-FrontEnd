using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace Model.Model
{
    public class UserModel
    {
        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Description { get; set; }
        
        [RegularExpression("^(male|female)$", ErrorMessage = "Gender must be 'male' or 'female'.")]
        public string Gender { get; set; }
        
        [RegularExpression("^(male|female)$", ErrorMessage = "Preference must be 'male' or 'female'.")]
        public string Preference { get; set; }
        
        public List<string> Pictures { get; set; }

        public UserModel(string email, string username, string firstName, string lastName, DateTime birthday, string description, string gender, string preference, List<string> pictures)
        {
            Email = email;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
            Description = description;
            Gender = gender;
            Preference = preference;
            Pictures = pictures;
        }

        public int Age()
        {
            DateTime now = DateTime.Today;
            int age = now.Year - Birthday.Year;
            if (Birthday > now.AddYears(-age))
                age--;
            return age;
        }
    }
}


    
    