using System.ComponentModel.DataAnnotations;

namespace TicketHive.Shared.Models
{
    public class BookingModel
    {
        [Key]
        public int Id { get; set; }
        public int EventId { get; set; }
        public string EventName { get; set; } = null!;
        public string UserId { get; set; } = null!;
        public int Quantity { get; set; }
        [DataType(DataType.DateTime)]
        public DateTime BookingDate { get; set; } = DateTime.Now;
    }
}
