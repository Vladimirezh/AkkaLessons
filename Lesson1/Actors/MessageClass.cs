namespace Lesson1.Actors
{
    public class MessageClass
    {
        public MessageClass(string text)
        {
            Text = text;
        }
        public string Text { private set; get; }
    }
}
