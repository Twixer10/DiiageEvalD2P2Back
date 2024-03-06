using EvalD2P2.Entity;
using Microsoft.EntityFrameworkCore;

namespace EvalD2P2.DAL;

public class EvalD2P2DbContext : DbContext
{
    public EvalD2P2DbContext(DbContextOptions<EvalD2P2DbContext> options) : base(options)
    {
    }
    
    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
    
    }
    
}