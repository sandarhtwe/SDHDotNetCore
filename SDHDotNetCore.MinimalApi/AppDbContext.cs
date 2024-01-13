using Microsoft.EntityFrameworkCore;
using SDHDotNetCore.MinimalApi.Models;

namespace SDHDotNetCore.MinimalApi.EFDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions options) : base(options)
        {
        }

        public DbSet<BlogDataModel> Blogs { get; set; }
    }
}
