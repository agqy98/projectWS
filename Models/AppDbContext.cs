using System;
using MySql.Data.EntityFramework;
using System.Data.Entity;
using System.Diagnostics;

namespace Project_WS.Models
{
    [DbConfigurationType(typeof(MySqlEFConfiguration))]
    public class AppDbContext:DbContext
    {
        public DbSet<Staff> staffs{ get; set; }

        public AppDbContext() : base("DefaultConnection")
        {
            Database.SetInitializer<AppDbContext>(new CreateDatabaseIfNotExists<AppDbContext>());
            Database.Log = s => Debug.WriteLine(s);
        }

        protected override void Dispose(bool disposing)
        {
            base.Dispose(disposing);
            // Additional disposal logic, if needed
        }

    }
}