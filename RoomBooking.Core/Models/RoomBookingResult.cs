using RoomBooking.Core.Enums;

namespace RoomBooking.Core.Domain;

public record RoomBookingResult : RoomBookingBase
{
    public BookingSuccessFlag Flag { get; set; }
}
