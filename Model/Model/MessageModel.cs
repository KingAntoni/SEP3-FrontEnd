namespace Model.Model;

public class MessageModel
{
    public string Message { get; set; }

    public string Username { get; set; }

    public string TimeStamp { get; set; }

    public MessageModel(string message, string username, string timeStamp)
    {
        Message = message;
        Username = username;
        TimeStamp = timeStamp;
    }

    public override string ToString()
    {
        return "Message: " + Message + " Username: " + Username + " TimeStamp: " + TimeStamp;
    }
}