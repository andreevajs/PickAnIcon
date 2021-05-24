using PickAnIcon.Database.Entities;
using PickAnIcon.Repositories;
using System;
using System.Linq;

namespace PickAnIcon.Services
{
    public interface IUsersService
    {
        public Result Register(string username, string password);
        public Result Login(string username, string password);

        public Result<User> GetByUserId(int id);
        public Result<User> GetByName(string username);
    }

    public class UsersService : IUsersService
    {
        public IUsersRepository _repository;

        public UsersService(IUsersRepository repository)
        {
            _repository = repository;
        }

        public Result Login(string username, string password)
        {
            var user = _repository.GetByUsername(username);

            if (user == null)
                return Result.Error("user doesn't exist");

            if (user.PasswordHash == BCrypt.Net.BCrypt.HashPassword(password, user.Salt))
                return Result.Success();
            else
                return Result.Error("invalid password");
                  
        }

        public Result Register(string username, string password)
        {
            if (_repository.GetByUsername(username) != null)
                return Result.Error("username already exists");

            var salt = BCrypt.Net.BCrypt.GenerateSalt();
            var user = new User()
            {
                Username = username,
                PasswordHash = BCrypt.Net.BCrypt.HashPassword(password, salt),
                Salt = salt
            };

            _repository.Add(user);
            return Result.Success();
        }

        public Result<User> GetByUserId(int id)
        {
            var user = _repository.GetById(id);

            if (user == null)
            {
                return Result<User>.Error("user with that user id not found");
            }

            return Result<User>.Success().WithValue(user);
        }

        public Result<User> GetByName(string username)
        {
            if (string.IsNullOrEmpty(username))
            {
                return Result<User>.Error("user name is null or empty");
            }

            var user = _repository.GetByUsername(username);

            if (user == null)
            {
                return Result<User>.Error("user with that user id not found");
            }

            return Result<User>.Success().WithValue(user);
        }
    }
}
