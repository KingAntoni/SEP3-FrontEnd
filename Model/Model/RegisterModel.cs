using System.Runtime.InteropServices.JavaScript;

namespace Model.Model;

public class RegisterModel
{
    public string Email { get; set; }
    public string Username { get; set; }
    public string Password { get; set; }
    public string ConfirmPassword { get; set;}
    
    public string FirstName { get; set; }
    
    public string LastName { get; set; }
    
    public DateTime Birthday { get; set; }
    
    public string Description { get; set; }

    public override string ToString()
    {
        return "Email: " + Email + " Username: " + Username + " Password: " + Password + " ConfirmPassword: " + ConfirmPassword + " FirstName: " + FirstName + " LastName: " + LastName + " Birthday: " + Birthday + " Description: " + Description;
    }
        
    }
    