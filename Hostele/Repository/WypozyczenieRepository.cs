using Hostele.Data;
using Hostele.Models;

namespace Hostele.Repository;

public class WypozyczenieRepository : Repository<Wypozyczenie>, IWypozyczenieRepository
{
    private readonly ApplicationDbContext _db;

    public WypozyczenieRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Wypozyczenie obj)
    {
        _db.Wypozyczenia.Update(obj);
    }

}
