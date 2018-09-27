#region Using
using System;
using System.Linq;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using testing_net.Models;
#endregion

namespace testing_net.Repositories.Database
{
    public class DataBaseContext : IdentityDbContext<ApplicationUser>
    {
        public DataBaseContext(DbContextOptions<DataBaseContext> options) : base(options) {}

        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder) => base.OnModelCreating(modelBuilder);
    }
}
