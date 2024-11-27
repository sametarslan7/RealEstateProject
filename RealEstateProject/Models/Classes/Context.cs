using System;
using System.Collections.Generic;
using System.Data.Entity;
using System.Linq;
using System.Web;

namespace RealEstateProject.Models.Classes
{
    public class Context : DbContext
    {
        public DbSet<Build> Builds { get; set; }
        public DbSet<Image> Images { get; set; }
        public DbSet<BuildType> BuildTypes { get; set; }
        public DbSet<Contact> Contacts { get; set; }
        public DbSet<Office> Offices { get; set; }
        public DbSet<Project> Projects { get; set; }
        public DbSet<Admin> Admins { get; set; }
        public DbSet<Message> Messages { get; set; }
        protected override void OnModelCreating(DbModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            //build-image one to many relationship
            modelBuilder.Entity<Image>()
                .HasRequired(p=>p.Build)
                .WithMany(c=>c.Images)
                .HasForeignKey(p=>p.BuildId);

            //build-buildtype one to many relationship
            modelBuilder .Entity<Build>()
                .HasRequired(c => c.buildType)
                .WithMany(f => f.Builds)
                .HasForeignKey(c => c.buildTypeId);
        }

    }
}