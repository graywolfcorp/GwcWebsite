using System.Threading.Tasks;
using GWC.Web.Interfaces.Data;

namespace GWC.DataAccess
{
    public class GwcUnitOfWork : IGwcUnitOfWork
    {
        private readonly IGwcDbContext _dbContext;

        public GwcUnitOfWork(IGwcDbContext dbContext)
        {
            _dbContext = dbContext;
        }

        public IGwcDbContext Context
        {
            get { return _dbContext; }
        }

        public void Commit()
        {
            _dbContext.SaveChanges();
        }

        public Task<int> CommitAsync()
        {
            return _dbContext.SaveChangesAsync();
        }

    }
}
