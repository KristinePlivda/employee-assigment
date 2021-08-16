using HrDataRegistration.DataAccess.Models;
using Microsoft.EntityFrameworkCore;
using System;

namespace HrDataRegistration.DataAccess
{
    public class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Employee>().HasData(
                new Employee { Id = 1, FirstName = "Meredith", LastName = "White", SocialSecurityNumber = "21019311334", PhoneNumber = "20239184", CreationDate = DateTime.Parse("2021-08-01") },
                new Employee { Id = 2, FirstName = "Cindy", LastName = "Carter", SocialSecurityNumber = "21039111234", PhoneNumber = "20239184", CreationDate = DateTime.Parse("2021-08-01") },
                new Employee { Id = 3, FirstName = "Bart", LastName = "Li", SocialSecurityNumber = "22019611334", PhoneNumber = "20239444", CreationDate = DateTime.Parse("2021-08-01") },
                new Employee { Id = 4, FirstName = "Jane", LastName = "Honey", SocialSecurityNumber = "02059315335", PhoneNumber = "20255184", CreationDate = DateTime.Parse("2021-08-01") },
                new Employee { Id = 5, FirstName = "Matt", LastName = "Li", SocialSecurityNumber = "21048811334", PhoneNumber = "65439184", CreationDate = DateTime.Parse("2021-08-01") },
                new Employee { Id = 6, FirstName = "Tiffany", LastName = "Justice", SocialSecurityNumber = "21014211334", PhoneNumber = "20453184", CreationDate = DateTime.Parse("2021-08-01") },
                new Employee { Id = 7, FirstName = "Laura", LastName = "Olive", SocialSecurityNumber = "23014312234", PhoneNumber = "12339184", CreationDate = DateTime.Parse("2021-08-01") },
                new Employee { Id = 8, FirstName = "Daniel", LastName = "Mean", SocialSecurityNumber = "21067721234", PhoneNumber = "32984738", CreationDate = DateTime.Parse("2021-08-01"), EndDate = DateTime.Parse("2021-08-03") }
            );
        }

        public DbSet<Employee> Employees { get; set; }
    }
}