using BuberDinner.Domain.Entities;

namespace BuberDinner.Application.Common.Interface.Persistence
{
    public interface IUserRepository
    {
        void Add(User user);
        User? GetUserByEmail(string email);
    }
}