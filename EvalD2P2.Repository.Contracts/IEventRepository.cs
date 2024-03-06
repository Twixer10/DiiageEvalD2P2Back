using EvalD2P2.Entity;

namespace EvalD2P2.Repository.Contract;

public interface IEventRepository
{
    public Task<Event> AddEvent(Event newEvent);
}