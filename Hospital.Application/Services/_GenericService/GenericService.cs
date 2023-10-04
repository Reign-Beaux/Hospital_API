using Hospital.Infraestructure.Persistences.UnitsOfWork;

namespace Hospital.Application.Services._GenericService
{
  public class GenericService
  {
    private protected readonly IUnitOfWork _unitOfWork;

    public GenericService(IUnitOfWork unitOfWork)
      => _unitOfWork = unitOfWork;
  }
}
