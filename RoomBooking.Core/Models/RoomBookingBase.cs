namespace RoomBooking.Core.Models;

public abstract record RoomBookingBase
{
    public string FullName { get; set; }
    public string Email { get; set; }
    public DateTime Date { get; set; }
}
