using Azure;
using FrogExebitionAPI.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Hosting;

namespace FrogExebitionAPI.Database
{
    public class ApplicationContext : IdentityDbContext
    {
        public DbSet<Frog> Frogs { get; set; }
        public DbSet<Exebition> Exebitions { get; set; }
        public DbSet<FrogOnExebition> FrogsOnExebitions { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Vote> Votes { get; set; }

        public ApplicationContext(DbContextOptions<ApplicationContext> options)
            : base(options)
        {
            //Database.EnsureDeleted();
            Database.EnsureCreated();
        }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //many to many with join table
            modelBuilder.Entity<Frog>()
                .HasMany(e => e.Exebitions)
                .WithMany(e => e.Frogs)
                .UsingEntity<FrogOnExebition>();

            //No duplicate frogs on exebiton
            modelBuilder.Entity<FrogOnExebition>()
                .HasIndex(e => new { e.FrogId, e.ExebitionId }, "UniqueFrogId_ExebitionId").IsUnique(true);

            //User can vote for certain frog on exebition only once
            modelBuilder.Entity<Vote>()
                .HasIndex(e => new { e.UserId, e.FrogOnExebitionId }, "UniqueUserId_FrogOnExebitionId").IsUnique(true);
        }
    }
}
