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
        modelBuilder.Entity<Event>(entity =>
        {
            entity.ToTable("Event");
            entity.HasKey(e => e.Id);
            entity.Property(e => e.Title).IsRequired().HasColumnName("title");
            entity.Property(e => e.Description).IsRequired().HasColumnName("description");
            entity.Property(e => e.Date).IsRequired().HasColumnName("date");
            entity.Property(e => e.Location).IsRequired().HasColumnName("location");
        });
    }
}