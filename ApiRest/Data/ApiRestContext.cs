#nullable disable
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using Microsoft.EntityFrameworkCore;
using ApiRest.Models;
using System.Data.SqlTypes;

namespace ApiRest.Data
{
    public class ApiRestContext : DbContext
    {
        public ApiRestContext(DbContextOptions<ApiRestContext> options)
            : base(options)
        {
           
           
        }

        public DbSet<ApiRest.Models.Author> Author { get; set; }

        public DbSet<ApiRest.Models.Book> Book { get; set; }

        public DbSet<ApiRest.Models.Editorial> Editorial { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.Entity<Editorial>().HasQueryFilter(p => EF.Property<DateTime?>(p, "DeletedAt") == null);
            modelBuilder.Entity<Book>().HasQueryFilter(p => EF.Property<DateTime?>(p, "DeletedAt") == null);
            modelBuilder.Entity<Author>().HasQueryFilter(p => EF.Property<DateTime?>(p, "DeletedAt") == null);
          //  modelBuilder.Entity<Author>().Ignore(c => c.FullName);
            foreach (var entityType in modelBuilder.Model.GetEntityTypes())
            {
                if (modelBuilder.Entity(entityType.Name).Property<DateTime>("CreatedAt") != null)
                {
                    modelBuilder.Entity(entityType.Name).Property("CreatedAt").HasDefaultValueSql("getdate()");
                }
                if (modelBuilder.Entity(entityType.Name).Property<DateTime>("UpdatedAt") != null)
                {
                    modelBuilder.Entity(entityType.Name).Property("UpdatedAt").HasDefaultValueSql("getdate()");
                }
            }
        }
        public virtual System.Threading.Tasks.Task<int> SaveChangesAsync()
        {
            var toDeleteEntities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Deleted && e.Metadata.GetProperties()
                .Any(x => x.Name == "DeletedAt"))
                .ToList();
            var toUpdateEntities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified && e.Metadata.GetProperties()
                .Any(x => x.Name == "UpdatedAt"))
                .ToList();

            foreach (var entity in toDeleteEntities)
            {
                entity.State = EntityState.Unchanged;
                entity.CurrentValues["DeletedAt"] = DateTime.Now;
            }
            foreach (var entity in toUpdateEntities)
            {
              //entity.CurrentValues["UpdatedAt"] = DateTime.UtcNow;
            }

            return base.SaveChangesAsync();
        }
        public override int SaveChanges()
        {
            // Borrado Suave 
            var toDeleteEntities = ChangeTracker.Entries()
                 .Where(e => e.State == EntityState.Deleted && e.Metadata.GetProperties()
                 .Any(x => x.Name == "DeletedAt"))
                 .ToList();
            var toUpdateEntities = ChangeTracker.Entries()
                .Where(e => e.State == EntityState.Modified && e.Metadata.GetProperties()
                .Any(x => x.Name == "UpdatedAt"))
                .ToList();

            foreach (var entity in toDeleteEntities)
            {
                entity.State = EntityState.Unchanged;
                entity.CurrentValues["DeletedAt"] = DateTime.Now;
            }
            foreach (var entity in toUpdateEntities)
            {
               // entity.CurrentValues["UpdatedAt"] = DateTime.UtcNow; 

            }

            return base.SaveChanges();
        }
    }
}
