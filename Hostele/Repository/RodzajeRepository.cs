using Hostele.Data;
using Hostele.Models;

namespace Hostele.Repository;

public class RodzajeRepository: Repository<Rodzaj>,IRodzajeRepository
{
    private readonly ApplicationDbContext _db;
    public RodzajeRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    public void Update(Rodzaj obj)
    {
        _db.Rodzaje.Update(obj);
    }
}