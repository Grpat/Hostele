using System.Linq;
using System.Linq.Expressions;
using Hostele.Data;
using Hostele.Models;
using Microsoft.EntityFrameworkCore;

namespace Hostele.Repository;

public class PokojRepository:Repository<Pokoj>,IPokojRepository
{
    private readonly ApplicationDbContext _db;
    public PokojRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }
    public async Task <IEnumerable<Pokoj>> GetDostepnePokoje(List<int> zarezerwowanepokoje)
    {
        IQueryable<Pokoj> query = _db.Pokoje;
        if (zarezerwowanepokoje.Count!=0)
        {
            foreach (var pokojId in zarezerwowanepokoje)
            {
                query = query.Where(x=>x.Id!=pokojId);
            }
        }
        
        return await query.ToListAsync();
    }
    public void Update(Pokoj obj)
    {
        _db.Pokoje.Update(obj);
    }
}