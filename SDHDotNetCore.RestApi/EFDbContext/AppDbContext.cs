using Microsoft.EntityFrameworkCore;
using SDHDotNetCore.RestApi.Models;

namespace SDHDotNetCore.RestApi.EFDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}
