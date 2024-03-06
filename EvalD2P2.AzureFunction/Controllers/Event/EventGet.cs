using System.Net;
using EvalD2P2.Service.Contracts;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EvalD2P2.AzureFunctions.Controllers.Event;

public class EventGet
{
    private readonly IEventService _eventService;

    public EventGet(IEventService eventService)
    {
        this._eventService = eventService;
    }

    [Function("EventGet")]
    public async Task<HttpResponseData> Run(
        [HttpTrigger(AuthorizationLevel.Anonymous, "get", Route = "events")]
        HttpRequestData req,
        [FromQuery] int limit = 10,
        [FromQuery] int page = 1)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "application/json; charset=utf-8");
        if (limit <= 0 || page <= 0)
        {
            response.StatusCode = HttpStatusCode.BadRequest;
            await response.WriteStringAsync(
                JsonConvert.SerializeObject(
                    new { errorMessage = "Invalid limit or offset number. Cannot be less than 1." }));
        }
        else
        {
            try
            {
                var events = await _eventService.GetAllEvents(limit, page);
                var eventCount = await _eventService.GetEventCount();
                var result = new
                {
                    events,
                    eventCount
                };
                await response.WriteStringAsync(JsonConvert.SerializeObject(result));
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
        }


        return response;
    }
}