using Hostele.Models;

namespace Hostele.Repository;

public interface IPokojRepository:IRepository<Pokoj>
{
    Task<IEnumerable<Pokoj>> GetDostepnePokoje(List<int> zarezerwowanepokoje);
    void Update(Pokoj obj);
}