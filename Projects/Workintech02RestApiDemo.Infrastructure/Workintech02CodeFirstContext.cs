using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Workintech02RestApiDemo.Domain.Entities;

namespace Workintech02RestApiDemo.Infrastructure
{
    public class Workintech02CodeFirstContext : DbContext
    {
        public Workintech02CodeFirstContext()
        {
        }

        public Workintech02CodeFirstContext(DbContextOptions<Workintech02CodeFirstContext> options)
            : base(options)
        {
        }

        public DbSet<City> Cities { get; set; }
        public DbSet<Town> Towns { get; set; }
        public DbSet<User> Users { get; set; }
        public DbSet<Role> Roles { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }

        public DbSet<Movie> Movies { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            #region User

            modelBuilder.Entity<User>().HasKey(x => x.Id);
            modelBuilder.Entity<User>().Property(x => x.Id).UseIdentityColumn(1000, 2);

            modelBuilder.Entity<User>().Property(x => x.Name).IsRequired();

            modelBuilder.Entity<User>().Property(x => x.Email).IsRequired();
            modelBuilder.Entity<User>().Property(x => x.Password).IsRequired();

            modelBuilder.Entity<User>()
                .HasMany(x => x.UserRoles)
                .WithOne(x => x.User)
                .HasForeignKey(x => x.UserId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region Role
            modelBuilder.Entity<Role>().ToTable("Role");
            modelBuilder.Entity<Role>().HasKey(x => x.Id);
            modelBuilder.Entity<Role>().Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(250);

            modelBuilder.Entity<Role>()
                .HasMany(x => x.UserRoles)
                .WithOne(x => x.Role)
                .HasForeignKey(x => x.RoleId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion

            #region City
            modelBuilder.Entity<City>().ToTable("City","APP");
            modelBuilder.Entity<City>().HasKey(x => x.Id);
            modelBuilder.Entity<City>().Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(250);
            modelBuilder.Entity<City>()
                .HasMany(x => x.Towns)
                .WithOne(x => x.City)
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.Restrict);

            #endregion

            #region Town
            modelBuilder.Entity<Town>().ToTable("Town", "APP");
            modelBuilder.Entity<Town>().HasKey(x => x.Id);
            modelBuilder.Entity<Town>().Property(x => x.Name)
                    .IsRequired()
                    .HasMaxLength(250);

            modelBuilder.Entity<Town>()
                .HasOne(x => x.City)
                .WithMany(x => x.Towns)
                .HasForeignKey(x => x.CityId)
                .OnDelete(DeleteBehavior.Restrict);
            #endregion


            #region Movie
            modelBuilder.Entity<Movie>().ToTable("Movie", "APP");
            modelBuilder.Entity<Movie>().HasKey(x => x.Id);
            modelBuilder.Entity<Movie>().HasQueryFilter(x=>x.IsActive);

            #endregion


        }
    }
}
