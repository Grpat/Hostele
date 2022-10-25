using Hostele.Models;

namespace Hostele.Repository;

public interface IRodzajeRepository:IRepository<Rodzaj>
{
    void Update(Rodzaj obj);
}