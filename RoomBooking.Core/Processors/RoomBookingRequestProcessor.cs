namespace RoomBooking.Core.Processors;

public class RoomBookingRequestProcessor
{
    private readonly IRoomBookingService _roomBookingService;

    public RoomBookingRequestProcessor(IRoomBookingService roomBookingService)
    {
        this._roomBookingService = roomBookingService;
    }

    public RoomBookingResult BookRoom(RoomBookingRequest bookingRequest)
    {
        if (bookingRequest == null)
            throw new ArgumentNullException($"'{nameof(bookingRequest)}' can't be null");

        var availableRooms = _roomBookingService.GetAvailableRooms(bookingRequest.Date);

        if (availableRooms.Any())
        {
            var room = availableRooms.First();

            _roomBookingService.SaveBooking(CreateRoomBookingObject<RoomBook>(bookingRequest) with { RoomGuid = room.Guid });
        }

        return CreateRoomBookingObject<RoomBookingResult>(bookingRequest) with { Flag = availableRooms.Any() ? BookingSuccessFlag.Success : BookingSuccessFlag.Failure};
    }

    private TRoomBooking CreateRoomBookingObject<TRoomBooking>(RoomBookingRequest bookingRequest)
        where TRoomBooking : RoomBookingBase, new()
    {
        return new TRoomBooking
        {
            Date = bookingRequest.Date,
            Email = bookingRequest.Email,
            FullName = bookingRequest.FullName,
        };
    }
}
