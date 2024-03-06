using System.Collections.Generic;
using System.Net;
using EvalD2P2.Service.Contracts;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;

namespace EvalD2P2.AzureFunctions.Controllers.Event;

public class EventDelete
{
    private readonly IEventService _eventService;

    public EventDelete(IEventService eventService)
    {
        this._eventService = eventService;
    }

    [Function("EventDelete")]
    public async Task<HttpResponseData> DeleteEvent(
        [HttpTrigger(AuthorizationLevel.Anonymous, "delete", Route = "events/{id}")]
        HttpRequestData req,
        int id,
        FunctionContext executionContext)
    {
        var response = req.CreateResponse(HttpStatusCode.OK);
        response.Headers.Add("Content-Type", "application/json; charset=utf-8");

        if (id < 0)
        {
            response.StatusCode = HttpStatusCode.BadRequest;
            await response.WriteStringAsync("Invalid event id. Cannot be less than 0.");
        }
        else
        {
            try
            {
                var evnt = await _eventService.GetEvent(id);
                if (evnt != null)
                {
                    await _eventService.DeleteEvent(evnt);
                    response.StatusCode = HttpStatusCode.NoContent;
                }
                else
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    await response.WriteStringAsync("Event not found.");
                }
            }
            catch (Exception e)
            {
                response.StatusCode = HttpStatusCode.InternalServerError;
                await response.WriteStringAsync(e.Message);
            }
        }

        return response;
    }
}