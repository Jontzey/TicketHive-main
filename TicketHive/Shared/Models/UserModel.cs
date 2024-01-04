using System.ComponentModel.DataAnnotations;
using TicketHive.Server.Enums;

namespace TicketHive.Shared.Models;
public class UserModel
{
	[Key]
	public string Id { get; set; } = null!;
	public string Username { get; set; } = null!;
	public Country Country { get; set; }
	public List<BookingModel> Bookings { get; set; } = new();
}
