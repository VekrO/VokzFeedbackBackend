namespace VokzFeedback.Models
{
    public class Feedback
    {

        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public Usuario User { get; set; }
        public string Sender { get; set; }
        public string Status { get; set; }
        public DateTime DateHour { get; set; }

    }
}
