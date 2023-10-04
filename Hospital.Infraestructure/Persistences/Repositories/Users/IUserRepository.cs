using Hospital.Domain.Entities;

namespace Hospital.Infraestructure.Persistences.Repositories.Users
{
  public interface IUserRepository
  {
    Task<User> GetByUsername(string username);
  }
}
