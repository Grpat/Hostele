using Hostele.Models;

namespace Hostele.Repository;

public interface IWypozyczenieRepository:IRepository<Wypozyczenie>
{
    void Update(Wypozyczenie obj);
}
