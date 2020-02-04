using DAL.Models;
using Microsoft.EntityFrameworkCore;

namespace DAL.Context
{
    class DriversContext : DbContext
    {
        // TODO: Move to application

        private const string ConnectionString = @"Data Source=DR1N-MI\SQLEXPRESS;Initial Catalog=driversDb;Integrated Security=True;Connect Timeout=30;Encrypt=False;TrustServerCertificate=False;ApplicationIntent=ReadWrite;MultiSubnetFailover=False";

        internal DbSet<Driver> Drivers { get; set; }

        internal DbSet<Event> Events { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnectionString);
        }
    }
}