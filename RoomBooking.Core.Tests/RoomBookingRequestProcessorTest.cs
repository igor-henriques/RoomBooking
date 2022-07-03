namespace RoomBooking.Core;

public class RoomBookingRequestProcessorTest
{
    [Fact]
    public void Should_Return_Booking_Request_With_Request_Values()
    {
        //Arrange
        var bookingRequest = new RoomBookingRequest
        {
            FullName = "Igor Henriques",
            Email = "henriquesigor@yahoo.com.br",
            Date = new DateTime(2022, 10, 20)
        };

        var processor = new RoomBookingRequestProcessor();

        //Arrange
        RoomBookingResult result = processor.BookRoom(bookingRequest);

        //Assert
        result.ShouldNotBeNull(customMessage: "Resultado da requisição nulo");
        
        foreach (var requestProp in bookingRequest.GetType().GetProperties())
        {
            foreach (var resultProp in result.GetType().GetProperties())
            {
                if (requestProp.Name != resultProp.Name)
                    continue;

                requestProp.GetValue(bookingRequest).ShouldBe(resultProp.GetValue(result));
            }            
        }
    }
}
