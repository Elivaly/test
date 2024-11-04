namespace MyTest.Models
{
    public class Message
    {
        public int Id { get; set; }
        public DateTime Date { get; set; }
        public string Email { get; set; }
        public int Code { get; set; }
        public bool IsSent { get; set; } = false;
    }
}
