// covariant
IMessanger<EmailMessage> emailClient = new EmailMessanger();
IMessanger<Message> messageClient = emailClient;
var emailMessage = emailClient.MessageCreate("Hello world");
var messageMessage = messageClient.MessageCreate("Hello people");

emailMessage.Print();
messageMessage.Print();


// contrvariant
ISendMessanger<EmailMessage> outlookClient = new SimpleSendMessanger();
outlookClient.SendMessage(emailMessage);

ISendMessanger<Message> wathupClient = new SimpleSendMessanger();
outlookClient = wathupClient;
outlookClient.SendMessage(emailMessage);

// both
IGoodMessanger<EmailMessage, Message> messanger = new GoodMessanger();
Message message = messanger.MessageCreate("Good messanger create message");
messanger.SendMessage(message as EmailMessage);

IGoodMessanger<EmailMessage, EmailMessage> messangerEmail = new GoodMessanger();
var emailGoodMessage = messangerEmail.MessageCreate("Good messanger create email message");
messangerEmail.SendMessage(emailGoodMessage);

IGoodMessanger<Message, Message> goodMessanger = new GoodMessanger();
var goodMessage = goodMessanger.MessageCreate("Good messanger create good message");
goodMessanger.SendMessage(goodMessage);



interface IMessanger<out T>
{
    T MessageCreate(string text);
}


class EmailMessanger : IMessanger<EmailMessage>
{
    public EmailMessage MessageCreate(string text) => new EmailMessage(text);
}


interface ISendMessanger<in T>
{
    void SendMessage(T message);
}

class SimpleSendMessanger : ISendMessanger<Message>
{
    public void SendMessage(Message message)
    {
        Console.WriteLine($"Send message: {message.Text}");
    }
}

interface IGoodMessanger<in T1, out T2>
{
    T2 MessageCreate(string text);
    void SendMessage(T1 message);
}

class GoodMessanger : IGoodMessanger<Message, EmailMessage>
{
    public EmailMessage MessageCreate(string text) => new EmailMessage(text);
    public void SendMessage(Message message) => Console.WriteLine($"Send message: {message.Text}");
}


class Message
{
    public string? Text { set; get; }
    public Message(string? text) => this.Text = text;
    public virtual void Print() => Console.WriteLine($"Message: {this.Text}");
}

class EmailMessage : Message
{
    public EmailMessage(string text) : base(text) { }
    public virtual void Print() => Console.WriteLine($"Email Message: {this.Text}");
}

class SmsMessage : Message
{
    public SmsMessage(string text) : base(text) { }
    public virtual void print() => Console.WriteLine($"Sms Message: {this.Text}");
}


