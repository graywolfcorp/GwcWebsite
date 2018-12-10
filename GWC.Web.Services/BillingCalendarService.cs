using AutoMapper;
using GWC.Web.Dtos;
using GWC.Web.Interfaces.Data;
using GWC.Web.Interfaces.Services;
using GWC.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GWC.Web.Services
{
    public class BillingCalendarService : IBillingCalendarService
    {
        private IGwcUnitOfWork _unitOfWork;
        private IMapper _mapper;

        public BillingCalendarService(IGwcCommonService commonService, IMapper mapper)
        {
            _unitOfWork = commonService.UnitOfWork;
            _mapper = mapper;
        }


        public BillingCalendarDto GetById(int id)
        {
            var calendar = _unitOfWork.Context.GetDbSet<BillingCalendar>().FirstOrDefault(x => x.Id == id);
            return Mapper.Map<BillingCalendarDto>(calendar); ;
        }

        public IEnumerable<BillingCalendarDto> GetAll()
        {
            IQueryable<BillingCalendar> calendar = _unitOfWork.Context.GetDbSet<BillingCalendar>();
            return _mapper.Map<IEnumerable<BillingCalendarDto>>(calendar); ;
        }


    }
}