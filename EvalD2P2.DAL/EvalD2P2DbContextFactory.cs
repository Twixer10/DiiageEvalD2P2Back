using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;
using System.IO;

namespace EvalD2P2.DAL
{
    public class EvalD2P2DbContextFactory : IDesignTimeDbContextFactory<EvalD2P2DbContext>
    {
        public EvalD2P2DbContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<EvalD2P2DbContext>();
            optionsBuilder.UseSqlServer("Server=localhost;Database=eval;User Id=sa;Password=MyPass@word;TrustServerCertificate=True");

            return new EvalD2P2DbContext(optionsBuilder.Options);
        }
    }
}