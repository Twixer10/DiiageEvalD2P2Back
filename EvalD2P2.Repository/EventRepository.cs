using EvalD2P2.DAL;
using EvalD2P2.Entity;
using EvalD2P2.Repository.Contract;

namespace EvalD2P2.Repository;

public class EventRepository : IEventRepository
{
    private readonly EvalD2P2DbContext _dbContext;
    
    public EventRepository(EvalD2P2DbContext dbContext)
    {
        this._dbContext = dbContext;
    }
    
    public async Task<Event> AddEvent(Event newEvent)
    {
        var result = await this._dbContext.Events.AddAsync(newEvent);
        await this._dbContext.SaveChangesAsync();
        return result.Entity;
    }
}