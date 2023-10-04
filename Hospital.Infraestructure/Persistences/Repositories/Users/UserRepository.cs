using Dapper;
using Hospital.Domain.Entities;
using Hospital.Infraestructure.Persistences.Repositories._GenericRepository;
using System.Data;

namespace Hospital.Infraestructure.Persistences.Repositories.Users
{
  public class UserRepository : GenericRepository, IUserRepository
  {
    public UserRepository(IDbTransaction dbTransaction) : base(dbTransaction)
    {
    }

    public async Task<User> GetByUsername(string username)
    {
      string spString = "[dbo].[Usp_Users_GET] @Username = @Username";
      return await _dbConnection.QuerySingleOrDefaultAsync<User>(
        spString,
        new {
          Username = username
        },
        transaction: _dbTransaction);
    }
  }
}
