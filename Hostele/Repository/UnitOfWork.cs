using System.ComponentModel;
using Hostele.Data;

namespace Hostele.Repository;

public class UnitOfWork:IUnitOfWork

{
    private readonly ApplicationDbContext _db;
    public UnitOfWork(ApplicationDbContext db) 
    {
        _db = db;
        Hostel = new HostelRepository(_db);
        Pokoj = new PokojRepository(_db);
        Rodzaj = new RodzajeRepository(_db);
        Wypozyczenie = new WypozyczenieRepository(_db);
    }
    public IHostelRepository Hostel{ get; private set; }
    public IPokojRepository Pokoj{ get; private set; }
    public IRodzajeRepository Rodzaj{ get; private set; }
    public IWypozyczenieRepository Wypozyczenie{ get;}
    
    public async Task Save()
    {
        await _db.SaveChangesAsync();
    }
}