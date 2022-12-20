using Microsoft.EntityFrameworkCore;
using ShiftMgtDbContext.Entities;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ShiftMgtDbContext.Data
{
    public class ShiftManagementDbContext : DbContext
    {
        public ShiftManagementDbContext(DbContextOptions options) : base(options)  
        
        {
                
        }

        public DbSet<Project> projects { get; set; }

        public DbSet<User> users { get; set; }

        public DbSet<ProjectDetail> projectDetails { get; set; }

        public DbSet<Shift> Shifts { get; set; }

       
        public DbSet<Comment> Comments { get; set; }

        public DbSet<UserCredential> UserCredentials { get; set; }

        public DbSet<UserRole> UserRoles { get; set; }
       
    }
}
