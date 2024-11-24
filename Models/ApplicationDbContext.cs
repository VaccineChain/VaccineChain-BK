using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Infrastructure;
using Microsoft.EntityFrameworkCore.Storage;
using NuGet.Protocol.Plugins;
using System.Collections.Generic;
using vaccine_chain_bk.Constraints;

namespace vaccine_chain_bk.Models
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
            try
            {
                if (Database.GetService<IDatabaseCreator>() is RelationalDatabaseCreator databaseCreator)
                {
                    if (!databaseCreator.CanConnect())
                    {
                        databaseCreator.Create();
                    }
                }
            }
            catch (Exception ex)
            {
                Console.WriteLine(ex.Message);
            }
        }

        public ApplicationDbContext()
        {

        }

        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }
        public DbSet<Vaccine> Vaccines { get; set; }
        public DbSet<Device> Devices { get; set; }
        public DbSet<Dose> Doses { get; set; }
        public DbSet<Log> Logs { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Seed Roles
            modelBuilder.Entity<Role>().HasData(
                new Role { RoleId = new Guid("7140fc1c-7e7b-4b5b-aeec-70ac91de934f"), Name = ERole.Admin.ToString() },
                new Role { RoleId = new Guid("be12fb13-04d6-4acf-8ad4-07927308bb6c"), Name = ERole.User.ToString() }
            );

            string adminPassword = "Admin@123";
            PasswordHasher<string> passwordHasher = new();
            string password = passwordHasher.HashPassword(null, adminPassword);

            // Seed Users
            modelBuilder.Entity<User>().HasData(
                new User
                {
                    UserId = new Guid("bc2e7c38-0214-4489-9dd2-eafbce0a71b0"),
                    Email = "admin@gmail.com",
                    Password = password,
                    FirstName = "Admin",
                    LastName = "Admin",
                    DateOfBirth = new DateTime(2002, 8, 5),
                    Address = "Admin Address",
                    Status = EAccount.Active,
                    RoleId = new Guid("7140fc1c-7e7b-4b5b-aeec-70ac91de934f")
                }
            );
        }
    }
}
