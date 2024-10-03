using EmployeeSystem.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System.Collections.Generic;

namespace EmployeeSystem.ApplicationDbContexts
{
    public class ApplicationDbContext : IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {

        }

        public DbSet<EmployeeModel> Employees { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<EmployeeModel>()
                .HasKey(e => e.EmployeeID);// Defining the primary key here.

             modelBuilder.Entity<EmployeeModel>()
            .Property(e => e.EmployeeID)
            .ValueGeneratedOnAdd();

            modelBuilder.Entity<EmployeeModel>()
            .Property(e => e.Salary)
            .HasColumnType("decimal(18, 2)");

             modelBuilder.Entity<EmployeeModel>()
        .HasIndex(e => e.Email)
        .IsUnique();  // Enforce unique email addresses

            base.OnModelCreating(modelBuilder);
        }
    }
}