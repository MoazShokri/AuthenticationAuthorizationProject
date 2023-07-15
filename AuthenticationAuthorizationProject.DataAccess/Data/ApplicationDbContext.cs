using AuthenticationAuthorizationProject.Models;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace AuthenticationAuthorizationProject.DataAccess.Data
{
    public class ApplicationDbContext :  IdentityDbContext<ApplicationUser>
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options) : base(options)
        {
                
        }
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            // Configure many-to-many relationship between Group and Permission
            modelBuilder.Entity<GroupPermission>()
                .HasKey(gp => new { gp.GroupId, gp.PermissionId });

            modelBuilder.Entity<GroupPermission>()
                .HasOne(gp => gp.Group)
                .WithMany()
                .HasForeignKey(gp => gp.GroupId);

            modelBuilder.Entity<GroupPermission>()
                .HasOne(gp => gp.Permission)
                .WithMany()
                .HasForeignKey(gp => gp.PermissionId);
        }
        public DbSet<Permission> Permissions { get; set; }
        public DbSet<Group> Groups { get; set; }
        public DbSet<GroupPermission> GroupPermissions { get; set; }
    }
}
