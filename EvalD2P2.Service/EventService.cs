using EvalD2P2.Entity;
using EvalD2P2.Repository.Contract;
using EvalD2P2.Service.Contracts;

namespace EvalD2P2.Service;

public class EventService : IEventService
{
    private readonly IEventRepository _eventRepository;

    public EventService(IEventRepository eventRepository)
    {
        this._eventRepository = eventRepository;
    }

    public Task<Event> AddEvent(Event newEvent)
    {
        return this._eventRepository.AddEvent(newEvent);
    }

    public Task<IEnumerable<Event>> GetAllEvents(int limit, int page)
    {
        return this._eventRepository.GetAllEvents(limit, page);
    }

    public Task<int> GetEventCount()
    {
        return this._eventRepository.GetEventCount();
    }
}