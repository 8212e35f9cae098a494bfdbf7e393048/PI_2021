using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Text;

namespace MVCMissingPersonAppValid.Data
{
    public class ApplicationDbContext : IdentityDbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<MissingPerson> MissingPeople { get; set; }

        protected override void OnModelCreating(ModelBuilder builder)
        {
            base.OnModelCreating(builder);
            List<MissingPerson> SampleData = new List<MissingPerson>();
            for (int i = 0; i < 35; i++)
            {
                SampleData.Add(new MissingPerson
                {
                    PersonID = i + 1,
                    FirstName = "Imie",
                    LastName = "Nazwisko",
                    Gender = 'M',
                    Age = 20,
                    Description = "Opis osoby zaginionej.",
                    Picture = "default.jpg",
                    City = "Warszawa"
                }); ;
            }
            builder.Entity<MissingPerson>().HasData(SampleData);
        }
    }
}
