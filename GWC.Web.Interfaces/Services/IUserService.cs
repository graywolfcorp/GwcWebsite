using GWC.Web.Dtos;
using System.Collections.Generic;


namespace GWC.Web.Interfaces.Services
{
    public interface IUserService
    {
        UserDto Authenticate(string username, string password);
        IEnumerable<UserDto> GetAll();
        UserDto GetById(int id);
        UserDto Create(UserDto user, string password);
        void Update(UserDto user, string password = null);
        void Delete(int id);
    }
}
