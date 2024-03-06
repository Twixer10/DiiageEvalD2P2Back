using EvalD2P2.Entity;
using EvalD2P2.Repository.Contract;

namespace EvalD2P2.Fixture;

public class EventFixture : IEventRepository
{
    public static List<Event> Events = new();

    public Task<Event?> AddEvent(Event? newEvent)
    {
        Events.Add(newEvent!);
        return Task.FromResult(newEvent);
    }

    public Task<IEnumerable<Event?>> GetAllEvents(int limit, int page)
    {
        var skip = (page - 1) * limit;
        return Task.FromResult(Events.Skip(skip).Take(limit).AsEnumerable());
    }

    public Task<int> GetEventCount()
    {
        return Task.FromResult(Events.Count);
    }

    public Task<Event?> EditEvent(Event? evnt)
    {
        var existingEvent = Events.FirstOrDefault(e => e.Id == evnt!.Id);
        if (existingEvent != null)
        {
            existingEvent.Title = evnt.Title;
            existingEvent.Description = evnt.Description;
            existingEvent.Location = evnt.Location;
            existingEvent.Date = evnt.Date;
        }

        return Task.FromResult(existingEvent);
    }

    public Task<Event?> GetEvent(int id)
    {
        return Task.FromResult(Events.FirstOrDefault(e => e.Id == id));
    }

    public Task<bool> DeleteEvent(Event evnt)
    {
        var existingEvent = Events.FirstOrDefault(e => e.Id == evnt.Id);
        if (existingEvent != null)
        {
            Events.Remove(existingEvent);
            return Task.FromResult(true);
        }

        return Task.FromResult(false);
    }
}