using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;

namespace eCommerceASP.Models
{
    public class AppDbContext : DbContext

    {
        public AppDbContext(DbContextOptions<AppDbContext>option) : base(option) { }
        
        public DbSet<ObjectToSend> ObjectsToSend { get; set; }
        public DbSet<Items> Items { get; set; }
        public DbSet<Users> Users { get; set; }
        public DbSet<Orderitems> Orderitems { get; set; }
        public DbSet<Addresses> Addresses { get; set; }
        public DbSet<Comments> Comments { get; set; }
        public DbSet<Order> Order { get; set; }
        






    }
}
