namespace Model.Model;

public class MessageModel
{
    public string Message { get; set; }

    public string Sender { get; set; }

    public string Receiver { get; set; }
    public string TimeStamp { get; set; }

    public MessageModel(string message, string username1,string username2, string timeStamp)
    {
        Message = message;
        Sender = username1;
        Receiver = username2;
        TimeStamp = timeStamp;
    }

    
    public override string ToString()
    {
        return "Message: " + Message + " Sender: " + Sender + " Receiver: " + Receiver +" TimeStamp: " + TimeStamp;
    }
}