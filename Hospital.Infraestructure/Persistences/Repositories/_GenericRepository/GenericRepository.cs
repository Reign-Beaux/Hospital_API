using System.Data;

namespace Hospital.Infraestructure.Persistences.Repositories._GenericRepository
{
  public class GenericRepository
  {
    private protected readonly IDbTransaction _dbTransaction;
    private protected readonly IDbConnection _dbConnection;

    public GenericRepository(IDbTransaction dbTransaction)
    {
      _dbTransaction = dbTransaction;
      _dbConnection = dbTransaction.Connection!;
    }
  }
}
