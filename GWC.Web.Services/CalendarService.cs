using AutoMapper;
using Gwc.Common.Interfaces.Services;
using GWC.Web.Dtos;
using GWC.Web.Dtos.Requests;
using GWC.Web.Interfaces.Data;
using GWC.Web.Interfaces.Services;
using GWC.Web.Model;
using System;
using System.Collections.Generic;
using System.Linq;

namespace GWC.Web.Services
{    
    public class CalendarService : ICalendarService
    {
        private IGwcUnitOfWork _unitOfWork;
        private IMapper _mapper;
        private ILoggingService _log;

        public CalendarService(IGwcCommonService commonService, IMapper mapper)
        {
            _unitOfWork = commonService.UnitOfWork;
            _mapper = mapper;            
            _log = commonService.LogManager.GetDefaultDbLoggingService();
        }

        public CalendarDto GetCalendarById(int id)
        {
            _log.Info($"GetCalendar {id}");
            var calendar = _unitOfWork.Context.GetDbSet<Calendar>().FirstOrDefault(x => x.Id == id);
            return _mapper.Map<CalendarDto>(calendar); ;
        }
        public IEnumerable<CalendarDto> SearchCalendar(CalendarSearchRequest request)
        {
            IQueryable<Calendar> calendar = _unitOfWork.Context.GetDbSet<Calendar>();
            return _mapper.Map<IEnumerable<CalendarDto>>(calendar); ;
        }

        public CalendarDto AddCalendar(CalendarDto calendarDto)
        {
            var calendar = new Calendar();
            try
            {
                calendar = _mapper.Map<CalendarDto, Calendar>(calendarDto);
                _unitOfWork.Context.GetDbSet<Calendar>().Add(calendar);
                _unitOfWork.Context.SaveChanges();
            }
            catch (Exception ex)
            {
                _log.Error(ex);
            }

            return _mapper.Map<CalendarDto>(calendar);
        }

        public CalendarDto UpdateCalendar(CalendarDto calendarDto)
        {
            var calendar = _unitOfWork.Context.GetDbSet<Calendar>().FirstOrDefault(x => x.Id == calendarDto.Id);
            calendar = _mapper.Map<CalendarDto, Calendar>(calendarDto);
            _unitOfWork.Context.SaveChanges();
            return _mapper.Map<CalendarDto>(calendar);
        }

        public void DeleteCalendar(int id   )
        {
            var calendar = _unitOfWork.Context.GetDbSet<Calendar>().FirstOrDefault(x => x.Id == id);
            _unitOfWork.Context.GetDbSet<Calendar>().Remove(calendar);
            _unitOfWork.Context.SaveChanges();
            return;
        }
    }
}