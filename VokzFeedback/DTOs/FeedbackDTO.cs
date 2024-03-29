using VokzFeedback.Models;

namespace VokzFeedback.DTOs
{
    public class FeedbackDTO
    {
        public Guid Id { get; set; }
        public string Description { get; set; }
        public Guid UserId { get; set; }
        public string Sender { get; set; }
        public string Status { get; set; }
        public DateTime DateHour { get; set; }

    }
}
