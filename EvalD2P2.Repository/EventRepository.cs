using EvalD2P2.DAL;
using EvalD2P2.Entity;
using EvalD2P2.Repository.Contract;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;

namespace EvalD2P2.Repository;

public class EventRepository : IEventRepository
{
    private readonly EvalD2P2DbContext _dbContext;

    public EventRepository(EvalD2P2DbContext dbContext)
    {
        this._dbContext = dbContext;
    }

    public async Task<Event?> AddEvent(Event? newEvent)
    {
        var result = await this._dbContext.Events.AddAsync(newEvent);
        await this._dbContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<IEnumerable<Event?>> GetAllEvents(int limit, int page)
    {
        return await this._dbContext.Events.Skip((page - 1) * limit).Take(limit).ToListAsync();
    }

    public Task<int> GetEventCount()
    {
        return this._dbContext.Events.CountAsync();
    }

    public async Task<Event?> EditEvent(Event? newEvent)
    {
        var result = this._dbContext.Events.Update(newEvent);
        await this._dbContext.SaveChangesAsync();
        return result.Entity;
    }

    public async Task<Event?> GetEvent(int id)
    {
        return await this._dbContext.Events.FindAsync(id);
    }

    public async Task<bool> DeleteEvent(Event evnt)
    {
        this._dbContext.Events.Remove(evnt);
        await this._dbContext.SaveChangesAsync();
        return true;
    }
}