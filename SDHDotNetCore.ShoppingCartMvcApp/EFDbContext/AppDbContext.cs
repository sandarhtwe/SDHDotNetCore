using Microsoft.EntityFrameworkCore;
using SDHDotNetCore.ShoppingCartMvcApp.Models;
using System;
using System.Collections.Generic;
using System.Data.SqlClient;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace SDHDotNetCore.ShoppingCartMvcApp.EFDbContext
{
    public class AppDbContext : DbContext
    {
        public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
        {
        }

        public DbSet<ProductItemDataModel> ProductItems { get; set; }
    }
}
