namespace RoomBooking.Core;

public class RoomBookingRequestProcessorTest
{
    private readonly RoomBookingRequestProcessor _processor;
    private readonly Mock<IRoomBookingService> _roomBookingServiceMock;
    private readonly List<Room> _availableRooms;
    private readonly RoomBookingRequest _request;

    public RoomBookingRequestProcessorTest()
    {
        this._roomBookingServiceMock = new Mock<IRoomBookingService>();
        this._processor = new RoomBookingRequestProcessor(_roomBookingServiceMock.Object);
        this._availableRooms = new List<Room>()
        {
            new Room() { Guid = Guid.NewGuid() }
        };
        this._request = new RoomBookingRequest
        {
            FullName = "Igor Henriques",
            Email = "henriquesigor@yahoo.com.br",
            Date = new DateTime(2022, 10, 20)
        };
        _roomBookingServiceMock.Setup(x => x.GetAvailableRooms(_request.Date)).Returns(this._availableRooms);
    }

    [Fact]
    public void Should_Return_Booking_Request_With_Request_Values()
    {
        //Arrange
        RoomBookingResult result = _processor.BookRoom(_request);

        //Assert
        result.ShouldNotBeNull(customMessage: "Resultado da requisição nulo");

        var checkResult = result.CheckIfPropertiesMatch(_request);

        checkResult.ShouldBeTrue();
    }

    [Fact]
    public void Should_Throw_Exception_For_Null_Request()
    {
        var exception = Should.Throw<ArgumentNullException>(() => _processor.BookRoom(null));

        exception.ParamName.ShouldContain("bookingRequest");
    }

    [Fact]
    public void Should_Save_Room_Booking_Request()
    {
        RoomBook savedBooking = null;

        _roomBookingServiceMock.Setup(q => q.SaveBooking(It.IsAny<RoomBook>()))
            .Callback<RoomBook>(booking =>
            {
                savedBooking = booking;
            });

        _processor.BookRoom(_request);

        _roomBookingServiceMock.Verify(q => q.SaveBooking(It.IsAny<RoomBook>()), Times.Once);

        savedBooking.ShouldNotBeNull();

        var checkResult = savedBooking.CheckIfPropertiesMatch(_request);

        checkResult.ShouldBeTrue();
        savedBooking.RoomGuid.ShouldBe(_availableRooms.First().Guid);
    }

    [Fact]
    public void Should_Not_Save_Room_Booking_Request_If_None_Available()
    {
        _availableRooms.Clear();
        _processor.BookRoom(_request);
        _roomBookingServiceMock.Verify(q => q.SaveBooking(It.IsAny<RoomBook>()), Times.Never);
    }

    [Theory]
    [InlineData(BookingSuccessFlag.Failure, false)]
    [InlineData(BookingSuccessFlag.Success, true)]
    public void Should_Return_Success_Or_Failure_In_Result(BookingSuccessFlag bookingFlag, bool isAvailable)
    {
        if (!isAvailable)
            _availableRooms.Clear();

        var result = _processor.BookRoom(_request);

        bookingFlag.ShouldBe(result.Flag);
    }

    [Theory]
    [InlineData(1, true)]
    [InlineData(null, false)]
    public void Should_Return_RoomBookingId_In_Result(long? roomBookId, bool isAvailable)
    {
        if (!isAvailable)
            _availableRooms.Clear();

        _roomBookingServiceMock.Setup(q => q.SaveBooking(It.IsAny<RoomBook>()))
            .Callback<RoomBook>(booking =>
            {
                booking.Id = roomBookId.Value;
            });

        var result = _processor.BookRoom(_request);

        result.RoomBookId.ShouldBe(roomBookId);
    }
}

