using Hospital.Infraestructure.Persistences.Repositories.Users;

namespace Hospital.Infraestructure.Persistences.UnitsOfWork
{
  public interface IUnitOfWork : IDisposable
  {
    public IUserRepository UserRepository { get; }
    public void Commit();
  }
}
