using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.ChangeTracking;
using System.Threading.Tasks;

namespace GWC.Web.Interfaces.Data
{
    public interface IGwcDbContext
    {
        DbSet<T> GetDbSet<T>() where T : class;
        int SaveChanges();
        Task<int> SaveChangesAsync();
        EntityEntry<TEntity> Entry<TEntity>(TEntity entity) where TEntity : class;
    }
}
