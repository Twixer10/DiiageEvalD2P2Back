using System.Net;
using EvalD2P2.Service.Contracts;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Newtonsoft.Json;

namespace EvalD2P2.AzureFunctions.Controllers.Event;

public class EventPost
{
    private readonly IEventService _eventService;

    public EventPost(IEventService eventService)
    {
        this._eventService = eventService;
    }

    [Function("CreateEvent")]
    public async Task<HttpResponseData> CreateEvent(
        [HttpTrigger(AuthorizationLevel.Anonymous, "post", Route = "events")]
        HttpRequestData req,
        FunctionContext executionContext)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "application/json; charset=utf-8");

        try
        {
            var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
            var newEvent = JsonConvert.DeserializeObject<Entity.Event>(requestBody);

            if (newEvent != null)
            {
                var eventCreated = await this._eventService.AddEvent(newEvent);
                await response.WriteStringAsync(JsonConvert.SerializeObject(eventCreated));
                response.StatusCode = HttpStatusCode.Created;
            }
            else
            {
                response.StatusCode = HttpStatusCode.BadRequest;
            }
        }
        catch (Exception e)
        {
            response.StatusCode = HttpStatusCode.InternalServerError;

            var error = new
            {
                errorMessage = e.Message,
                stackTrace = e.StackTrace
            };

            await response.WriteStringAsync(JsonConvert.SerializeObject(error));
        }

        return response;
    }
}