using GWC.Web.Interfaces.Data;
using GWC.Web.Model;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using System;
using System.Linq;
using System.Threading.Tasks;

namespace GWC.Web.DataAccess
{
    public class GwcDbContext : DbContext, IGwcDbContext
    {
        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            foreach (var property in modelBuilder.Model.GetEntityTypes()
                .SelectMany(t => t.GetProperties())
                .Where(p => p.ClrType == typeof(string)))
                {
                    if (property.GetMaxLength() == null)
                        property.SetMaxLength(50);
                }
        }
        public virtual DbSet<BillingCalendar> BillingCalendar { get; set; }
        public virtual DbSet<Calendar> Calendars { get; set; }
        public virtual DbSet<GwcLog> GwcLogs { get; set; }
        public virtual DbSet<Source> Sources { get; set; }
        public virtual DbSet<User> Users { get; set; }

        public GwcDbContext(DbContextOptions<GwcDbContext> options) : base(options)
        {
        }
        public DbSet<T> GetDbSet<T>() where T : class
        {
            return Set<T>();
        }
        public override int SaveChanges()
        {
            return base.SaveChanges();
        }
        public async Task<int> SaveChangesAsync()
        {
            return await base.SaveChangesAsync();
        }
    }
}