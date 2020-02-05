using DAL.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace DAL.Context
{
    class DriversContext : DbContext
    {
        private readonly string ConnString;

        internal DbSet<Driver> Drivers { get; set; }

        internal DbSet<Event> Events { get; set; }

        public DriversContext(string connString)
        {
            if (string.IsNullOrEmpty(connString))
            {
                throw new ArgumentException(nameof(connString));
            }

            ConnString = connString;
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(ConnString);
        }
    }
}