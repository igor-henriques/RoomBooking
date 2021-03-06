namespace RoomBooking.Core.Domain;

public record RoomBookingResult : RoomBookingBase
{
    public BookingSuccessFlag Flag { get; set; }
    public long? RoomBookId { get; set; }    
}
