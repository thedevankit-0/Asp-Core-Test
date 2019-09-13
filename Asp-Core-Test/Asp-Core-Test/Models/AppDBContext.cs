using Asp_Core_Test.Models;
using Microsoft.EntityFrameworkCore;

namespace MvcFirstApp.Models
{
    #region "AppDBContext Class"
    public class AppDBContext : DbContext
    {
        public AppDBContext(DbContextOptions<AppDBContext> options) : base(options)
        {
        }

        //DbSet Created for tables to be used
        public DbSet<Student> Student { get; set; }

        public DbSet<PhdSubject> PhdSubject { get; set; }
    }
    #endregion
}