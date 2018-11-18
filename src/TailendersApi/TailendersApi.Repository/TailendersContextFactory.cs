using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace TailendersApi.Repository
{
    public class TailendersContextFactory : IDesignTimeDbContextFactory<TailendersContext>
    {
        public TailendersContext CreateDbContext(string[] args)
        {
            var optionsBuilder = new DbContextOptionsBuilder<TailendersContext>();
            optionsBuilder.UseSqlServer("Server=tcp:tailenders.database.windows.net,1433;Initial Catalog=tailendersuat;Persist Security Info=False;User ID=tailenders_aden;Password=6u6o8wjusQNS;MultipleActiveResultSets=False;Encrypt=True;TrustServerCertificate=False;Connection Timeout=30;");

            return new TailendersContext(optionsBuilder.Options);
        }
    }
}
