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

    public Task<List<Aktywnosc>> GetAll()
    {
        return Task.FromResult(_db.Aktywnosci.ToList());
    }

    public async void AddActivity(string user, DateTime activityTime, string description)
    {
        _db.Aktywnosci.Add(new Aktywnosc
        {
            User = user,
            CzasAktywnosci = activityTime,
            OpisAktywnosci = description
        });
        await _db.SaveChangesAsync();
    }
}