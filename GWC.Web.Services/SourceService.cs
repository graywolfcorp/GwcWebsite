using AutoMapper;
using GWC.Web.Dtos;
using GWC.Web.Interfaces.Data;
using GWC.Web.Interfaces.Services;
using GWC.Web.Model;
using System.Collections.Generic;
using System.Linq;

namespace GWC.Web.Services
{
    public class SourceService : ISourceService
    {
        private IGwcUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public SourceService(IGwcCommonService commonService, IMapper mapper)
        {
            _unitOfWork = commonService.UnitOfWork;
            _mapper = mapper;
        }

        public SourceDto GetSourceById(int id)
        {
            var source = _unitOfWork.Context.GetDbSet<Source>().FirstOrDefault(x => x.Id == id);
            return _mapper.Map<SourceDto>(source); ;
        }

        public IEnumerable<SourceDto> SearchSource()
        {
            IQueryable<Source> source = _unitOfWork.Context.GetDbSet<Source>();
            return _mapper.Map<IEnumerable<SourceDto>>(source); ;
        }

        public SourceDto AddSource(SourceDto sourceDto)
        {
            var source = new Source();
            source = _mapper.Map<SourceDto, Source>(sourceDto);
            source.ExternalId = sourceDto.SourceId;
            _unitOfWork.Context.GetDbSet<Source>().Add(source);
            _unitOfWork.Context.SaveChanges();
            return _mapper.Map<SourceDto>(source);
        }

        public SourceDto UpdateSource(SourceDto sourceDto)
        {
            var source = _unitOfWork.Context.GetDbSet<Source>().FirstOrDefault(x => x.Id == sourceDto.Id);
            source = _mapper.Map<SourceDto, Source>(sourceDto);
            _unitOfWork.Context.SaveChanges();
            return _mapper.Map<SourceDto>(source);
        }

        public void DeleteSource(int id)
        {
            var source = _unitOfWork.Context.GetDbSet<Source>().FirstOrDefault(x => x.Id == id);
            _unitOfWork.Context.GetDbSet<Source>().Remove(source);
            _unitOfWork.Context.SaveChanges();
            return;
        }
    }
}