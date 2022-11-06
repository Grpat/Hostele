using Hostele.Data;
using Hostele.Models;

namespace Hostele.Repository;

public interface IActivitiesRepository: IRepository<Aktywnosc>
{
    public void AddActivity(string user, DateTime activityTime, string description);
    public Task<List<Aktywnosc>> GetAll();
}