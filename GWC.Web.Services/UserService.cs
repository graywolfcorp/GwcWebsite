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
    public class UserService : IUserService
    {
        private IGwcUnitOfWork _unitOfWork;

        public UserService(IGwcCommonService commonService)
        {
            _unitOfWork = commonService.UnitOfWork;
        }

        public UserDto Authenticate(string username, string password)
        {
            if (string.IsNullOrEmpty(username) || string.IsNullOrEmpty(password))
                return null;

            var user = _unitOfWork.Context.GetDbSet<User>().FirstOrDefault(x => x.Username == username);

            // check if username exists
            if (user == null)
                return null;

            // check if password is correct
            if (!VerifyPasswordHash(password, user.PasswordHash, user.PasswordSalt))
                return null;

            // authentication successful
            return Mapper.Map<UserDto>(user); ;
        }

        public IEnumerable<UserDto> GetAll()
        {
            IQueryable<User> users = _unitOfWork.Context.GetDbSet<User>();
            return Mapper.Map<IEnumerable<UserDto>>(users); ;
        }

        public UserDto GetById(int id)
        {
            var user = _unitOfWork.Context.GetDbSet<User>().FirstOrDefault(x => x.Id == id);
            return Mapper.Map<UserDto>(user); ;
        }

        public UserDto GetByUsername(string username)
        {
            var user = _unitOfWork.Context.GetDbSet<User>().FirstOrDefault(x => x.Username == username);
            return Mapper.Map<UserDto>(user); 
        }


        public UserDto Create(UserDto userDto, string password)
        {
            var user = new User();

            Mapper.Map(userDto, user);
            // validation
            if (string.IsNullOrWhiteSpace(password))
                throw new Exception("Password is required");

            if (GetByUsername(userDto.Username) != null)
                throw new Exception("Username " + user.Username + " is already taken");

            byte[] passwordHash, passwordSalt;
            CreatePasswordHash(password, out passwordHash, out passwordSalt);

            user.PasswordHash = passwordHash;
            user.PasswordSalt = passwordSalt;

            _unitOfWork.Context.GetDbSet<User>().Add(user);
            _unitOfWork.Context.SaveChanges();

            return Mapper.Map<UserDto>(user);
        }

        public void Update(UserDto userDto, string password = null)
        {
            var user = _unitOfWork.Context.GetDbSet<User>().FirstOrDefault(x => x.Id == userDto.Id);

            if (user == null)
                throw new Exception("User not found");

            if (userDto.Username != user.Username)
            {
                // username has changed so check if the new username is already taken
                if (GetByUsername(userDto.Username) != null)
                    throw new Exception("Username " + userDto.Username + " is already taken");
            }

            // update user properties
            user.FirstName = userDto.FirstName;
            user.LastName = userDto.LastName;
            user.Username = userDto.Username;

            // update password if it was entered
            if (!string.IsNullOrWhiteSpace(password))
            {
                byte[] passwordHash, passwordSalt;
                CreatePasswordHash(password, out passwordHash, out passwordSalt);

                user.PasswordHash = passwordHash;
                user.PasswordSalt = passwordSalt;
            }

            _unitOfWork.Context.GetDbSet<User>().Update(user);
            _unitOfWork.Context.SaveChanges();
        }

        public void Delete(int id)
        {
            var user = _unitOfWork.Context.GetDbSet<User>().FirstOrDefault(x => x.Id == id);            
            if (user != null)
            {
                _unitOfWork.Context.GetDbSet<User>().Remove(user);
                _unitOfWork.Context.SaveChanges();
            }
        }

        // private helper methods

        private static void CreatePasswordHash(string password, out byte[] passwordHash, out byte[] passwordSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");

            using (var hmac = new System.Security.Cryptography.HMACSHA512())
            {
                passwordSalt = hmac.Key;
                passwordHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
            }
        }

        private static bool VerifyPasswordHash(string password, byte[] storedHash, byte[] storedSalt)
        {
            if (password == null) throw new ArgumentNullException("password");
            if (string.IsNullOrWhiteSpace(password)) throw new ArgumentException("Value cannot be empty or whitespace only string.", "password");
            if (storedHash.Length != 64) throw new ArgumentException("Invalid length of password hash (64 bytes expected).", "passwordHash");
            if (storedSalt.Length != 128) throw new ArgumentException("Invalid length of password salt (128 bytes expected).", "passwordHash");

            using (var hmac = new System.Security.Cryptography.HMACSHA512(storedSalt))
            {
                var computedHash = hmac.ComputeHash(System.Text.Encoding.UTF8.GetBytes(password));
                for (int i = 0; i < computedHash.Length; i++)
                {
                    if (computedHash[i] != storedHash[i]) return false;
                }
            }

            return true;
        }
    }
}