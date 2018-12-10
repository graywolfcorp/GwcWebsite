using AutoMapper;
using GWC.Web.Dtos;
using GWC.Web.Model;

namespace GWC.Web.Services.AutoMapper
{
    public class GwcModelDtoProfile : Profile
    {
        public GwcModelDtoProfile()
        {
            CreateMap<User, UserDto>().ReverseMap();
            CreateMap<BillingCalendar, BillingCalendarDto>().ReverseMap();
            CreateMap<Calendar, CalendarDto>().ReverseMap();
            CreateMap<Source, SourceDto>().ReverseMap();
        }
    }
}
