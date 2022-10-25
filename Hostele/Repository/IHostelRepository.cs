using Hostele.Models;

namespace Hostele.Repository;

public interface IHostelRepository:IRepository<Hostel>
{
    void Update(Hostel obj);
    
}