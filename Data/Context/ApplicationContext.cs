using Microsoft.Data.SqlClient;
using Microsoft.EntityFrameworkCore;
using System;
using TransportComp.Data.Models;

namespace TransportComp.Data.Context
{

    public class ApplicationContext : DbContext
    {
        public ApplicationContext()
        {
            Database.EnsureCreated();
        }

        protected override void OnConfiguring(DbContextOptionsBuilder optionsBuilder)
        {
            optionsBuilder.UseSqlServer(GetConnectionString());
        }

        /// <summary>
        /// Настройка подключения к базе данных
        /// </summary>
        /// <returns></returns>
        private string GetConnectionString()
        {
            try
            {
                return new SqlConnectionStringBuilder()
                {
                    DataSource = "127.0.0.1",
                    UserID = "sa",
                    Password = "yourStrong(!)Password",
                    InitialCatalog = "TransComp",
                    TrustServerCertificate = true
                }.ConnectionString;
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex);
                throw;
            }
        }

        public DbSet<Depot> Depots { get; set; }
        public DbSet<Truck> Trucks { get; set; }
        public DbSet<Driver> Drivers { get; set; }
        public DbSet<DriverPhoto> DriverPhotos { get; set; }
        public DbSet<TruckPhoto> TruckPhotos { get; set; }
        public DbSet<Route> Routes { get; set; }
    }
}