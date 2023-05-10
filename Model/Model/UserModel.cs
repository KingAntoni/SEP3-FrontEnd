namespace Model.Model;

public class UserModel
{
    public UserModel(string email, string username, string firstName, string lastName, DateTime birthday, string description)
    {
        Email = email;
        Username = username;
        FirstName = firstName;
        LastName = lastName;
        Birthday = birthday;
        Description = description;
    }

    public string Email { get; set; }
    
    public string Username { get; set; }

    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public DateTime Birthday { get; set; }
    
    public string Description { get; set; }
    
    
}