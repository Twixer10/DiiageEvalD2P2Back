using System.Collections.Generic;
using System.Net;
using EvalD2P2.Service.Contracts;
using Microsoft.Azure.Functions.Worker;
using Microsoft.Azure.Functions.Worker.Http;
using Microsoft.Extensions.Logging;
using Newtonsoft.Json;

namespace EvalD2P2.AzureFunctions.Controllers.Event;

public class EventPut
{
    private readonly IEventService _eventService;

    public EventPut(IEventService eventService)
    {
        _eventService = eventService;
    }

    [Function("EditEvent")]
    public async Task<HttpResponseData> EditEvent(
        [HttpTrigger(AuthorizationLevel.Anonymous, "put", Route = "events/{id}")]
        HttpRequestData req,
        int id)
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
                    var requestBody = await new StreamReader(req.Body).ReadToEndAsync();
                    var updatedEvent = JsonConvert.DeserializeObject<Entity.Event>(requestBody);

                    evnt.Date = updatedEvent.Date;
                    evnt.Description = updatedEvent.Description;
                    evnt.Title = updatedEvent.Title;
                    evnt.Location = updatedEvent.Location;
                    if (updatedEvent != null)
                    {
                        var eventUpdated = await _eventService.EditEvent(evnt);
                        await response.WriteStringAsync(JsonConvert.SerializeObject(eventUpdated));
                    }
                    else
                    {
                        response.StatusCode = HttpStatusCode.BadRequest;
                    }
                }
                else
                {
                    response.StatusCode = HttpStatusCode.NotFound;
                    await response.WriteStringAsync("Event Not Found");
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
        }

        return response;
    }
}