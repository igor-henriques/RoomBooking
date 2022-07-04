namespace RoomBooking.Core.Domain;

public record RoomBook : RoomBookingBase
{
    public Guid RoomGuid { get; set; }
}
