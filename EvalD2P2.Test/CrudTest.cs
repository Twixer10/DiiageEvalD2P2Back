using NUnit.Framework;
using Moq;
using EvalD2P2.Service.Contracts;
using Microsoft.Azure.Functions.Worker.Http;
using EvalD2P2.AzureFunctions.Controllers.Event;
using Microsoft.Azure.Functions.Worker;
using System.Net;
using Newtonsoft.Json;
using System.Text;
using System.IO;

namespace EvalD2P2.Test
{
    public class EventPostTests
    {
        private Mock<IEventService> _eventServiceMock;
        private EventPost _eventPost;

        [SetUp]
        public void Setup()
        {
            _eventServiceMock = new Mock<IEventService>();
            _eventPost = new EventPost(_eventServiceMock.Object);
        }

        [Test]
        public async Task TestCreateEvent()
        {
            // Arrange
            var newEvent = new EvalD2P2.Entity.Event
            {
                Title = "Test Event",
                Description = "This is a test event",
                Location = "Test Location",
                Date = DateTime.Now
            };

            var req = new Mock<HttpRequestData>();
            var httpResponseMock = new Mock<HttpResponseData>(new Mock<FunctionContext>(MockBehavior.Strict).Object);
            httpResponseMock.SetupProperty(r => r.StatusCode);
            req.Setup(r => r.CreateResponse(HttpStatusCode.Created)).Returns(httpResponseMock.Object);
            req.Setup(r => r.Body)
                .Returns(new MemoryStream(Encoding.UTF8.GetBytes(JsonConvert.SerializeObject(newEvent))));

            _eventServiceMock.Setup(s => s.AddEvent(newEvent)).ReturnsAsync(newEvent);

            // Act
            var response =
                await _eventPost.CreateEvent(req.Object, new Mock<FunctionContext>(MockBehavior.Strict).Object);

            // Assert
            Assert.AreEqual(HttpStatusCode.Created, response.StatusCode);
            _eventServiceMock.Verify(s => s.AddEvent(newEvent), Times.Once);
        }
    }
}