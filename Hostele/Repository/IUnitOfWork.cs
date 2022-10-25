namespace Hostele.Repository;

public interface IUnitOfWork
{
    IHostelRepository Hostel { get;}
    IPokojRepository Pokoj{ get;}
    IRodzajeRepository Rodzaj{ get;}
    IWypozyczenieRepository Wypozyczenie{ get;}
    Task Save();
}