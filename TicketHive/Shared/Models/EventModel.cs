using System.ComponentModel.DataAnnotations;
using TicketHive.Shared.Enums;

namespace TicketHive.Shared.Models;
public class EventModel
{
    [Key]
    public int Id { get; set; }
    [Required(ErrorMessage = "Please enter name!")]
    public string Name { get; set; } = null!;
    [Required(ErrorMessage = "Please choose Eventype!")]
    public EventType EventType { get; set; }
    [Required(ErrorMessage = "Please enter number of tickets!")]
    public int NumberOfTickets { get; set; }
    [Required(ErrorMessage = "Please enter description!")]
    public string Description { get; set; } = null!;
    [Required(ErrorMessage = "Please enter price!")]
    public decimal Price { get; set; }


    [DataType(DataType.DateTime)]
    public DateTime StartTime { get; set; }

    [DataType(DataType.DateTime)]
    public DateTime EndTime { get; set; }
    [Required(ErrorMessage = "Please enter location!")]
    public string Location { get; set; } = null!;
    [Required(ErrorMessage = "Please enter host!")]
    public string Host { get; set; } = null!;
    public string ImageUrl { get; set; } = null!;
    public List<BookingModel> Bookings { get; set; } = new();
}
