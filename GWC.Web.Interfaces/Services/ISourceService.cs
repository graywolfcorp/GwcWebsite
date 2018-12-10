using GWC.Web.Dtos;
using System.Collections.Generic;

namespace GWC.Web.Interfaces.Services
{
    public interface ISourceService
    {
        IEnumerable<SourceDto> SearchSource();
        SourceDto GetSourceById(int id);
        SourceDto AddSource(SourceDto sourceDto);
        SourceDto UpdateSource(SourceDto sourceDto);
        void DeleteSource(int id);
    }
}