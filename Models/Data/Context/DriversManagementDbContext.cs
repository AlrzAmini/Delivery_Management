using DriversManagement.Models.Data.Entities;
using Microsoft.EntityFrameworkCore;
using System.Net.Sockets;
using System.Reflection.Metadata;
using System.Reflection;

namespace DriversManagement.Models.Data.Context
{
    public class DriversManagementDbContext : DbContext
    {
        public DriversManagementDbContext(DbContextOptions<DriversManagementDbContext> option) : base(option)
        {

        }

        #region entities

        public DbSet<User> Users { get; set; }

        public DbSet<Role> Roles { get; set; }

        public DbSet<Car> Car { get; set; }

        public DbSet<Shipment> Shipments { get; set; }

        public DbSet<Delivery> Deliveries { get; set; }


        #endregion


        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            foreach (var rel in modelBuilder.Model.GetEntityTypes().SelectMany(s => s.GetForeignKeys()))
            {
                rel.DeleteBehavior = DeleteBehavior.Restrict;
            }



            modelBuilder.Entity<User>()
                .HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<Role>()
                .HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<Car>()
                .HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<Shipment>()
                .HasQueryFilter(e => !e.IsDeleted);

            modelBuilder.Entity<Delivery>()
                .HasQueryFilter(e => !e.IsDeleted);

            base.OnModelCreating(modelBuilder);
        }
    }

}
