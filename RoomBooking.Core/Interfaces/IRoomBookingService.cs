
namespace RoomBooking.Core.Interfaces;

public interface IRoomBookingService
{
    void SaveBooking(RoomBook bookingRequest);
    IEnumerable<Room> GetAvailableRooms(DateTime date);
}
