using Hostele.Data;
using Hostele.Models;

namespace Hostele.Repository;

public interface IActivitiesRepository: IRepository<Aktywnosc>
{
    public List<Aktywnosc> GetAll();
}