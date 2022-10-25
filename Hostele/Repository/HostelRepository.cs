using Hostele.Data;
using Hostele.Models;
using Microsoft.EntityFrameworkCore;

namespace Hostele.Repository;

public class HostelRepository: Repository<Hostel>,IHostelRepository
{
    private readonly ApplicationDbContext _db;
    public HostelRepository(ApplicationDbContext db) : base(db)
    {
        _db = db;
    }

    
    
    public void Update(Hostel obj)
    {
        _db.Hostele.Update(obj);
    }

    
}