using HWDotNetCore.RestApi.Models;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace HWDotNetCore.RestAPI.EFDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<StudentDataModel> Blogs { get; set; }
    }
}
