namespace RoomBooking.Core.Models;

public record RoomBookingResult
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public DateTime Date { get; set; }
}
