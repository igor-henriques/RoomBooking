namespace RoomBooking.Core.Domain;

public record RoomBook : RoomBookingBase
{
    public long Id { get; set; }
    public Guid RoomGuid { get; set; }    
}
