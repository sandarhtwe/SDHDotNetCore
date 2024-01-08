using Microsoft.EntityFrameworkCore;
using SDHDotNetCore.ATMWebApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDHDotNetCore.ATMWebApp.EFDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ATMUserModel> Users { get; set; }
    }
}
