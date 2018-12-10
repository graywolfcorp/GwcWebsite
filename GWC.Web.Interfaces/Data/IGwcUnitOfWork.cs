using GWC.Web.Interfaces.Data;
using System.Threading.Tasks;

namespace GWC.Web.Interfaces.Data
{
    public interface IGwcUnitOfWork
    {
        IGwcDbContext Context { get; }
        void Commit();
        Task<int> CommitAsync();
    }
}
