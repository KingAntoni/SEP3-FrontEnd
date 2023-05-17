using System.Collections;

namespace Model.Model
{
    public class UserModel
    {
        private ArrayList _pictures;

        public UserModel(string email, string username, string firstName, string lastName, DateTime birthday, string description, char gender, char preference)
        {
            Email = email;
            Username = username;
            FirstName = firstName;
            LastName = lastName;
            Birthday = birthday;
            Description = description;
            Gender = gender;
            Preference = preference;
            // PictureUrl = pictureUrl;
        }

        public string Email { get; set; }
        public string Username { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime Birthday { get; set; }
        public string Description { get; set; }
        
        public char Gender { get; set; }
        
        public char Preference { get; set; }

        public ArrayList Pictures
        {
            get => _pictures;
            set => _pictures = value;
        }

        public int Age()
        {
            DateTime now = DateTime.Today;
            int age = now.Year - Birthday.Year;
            if (Birthday > now.AddYears(-age)) age--;
            return age;
        }
        }
    
}

    
    