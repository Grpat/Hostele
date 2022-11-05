using Hostele.Data;
using Hostele.Models;

namespace Hostele.Repository;

public class ActivitiesRepository: Repository<Aktywnosc>, IActivitiesRepository
{
    private readonly ApplicationDbContext _db;

    public ActivitiesRepository(ApplicationDbContext db): base(db)
    {
        _db = db;
    }

    public List<Aktywnosc> GetAll()
    {
        return _db.Aktywnosci;
    }
}