namespace Model.Model;

public class MatchModel
{
    public String FirstName { get; }
    public int Age { get; }
    public String Description { get; }
    
    public Boolean IsLiked { get; set; }


    public MatchModel(String firstName, int age, String description)
    {
        FirstName = firstName;
        Age = age;
        Description = description;
    }

    public override string ToString()
    {
        return "FirstName: " + FirstName + " Age: " + Age + " Description: " + Description;
    }
        
    
}