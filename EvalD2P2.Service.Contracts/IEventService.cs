using EvalD2P2.Entity;

namespace EvalD2P2.Service.Contracts;

public interface IEventService
{
    public Task<Event?> AddEvent(Event? newEvent);
    public Task<IEnumerable<Event?>> GetAllEvents(int limit, int page);
    public Task<int> GetEventCount();
    public Task<Event?> EditEvent(Event? evnt);
    public Task<Event?> GetEvent(int id);
    public Task<bool> DeleteEvent(Event evnt);
    
}