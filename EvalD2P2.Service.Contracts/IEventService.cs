using EvalD2P2.Entity;

namespace EvalD2P2.Service.Contracts;

public interface IEventService
{
    public Task<Event> AddEvent(Event newEvent);
}