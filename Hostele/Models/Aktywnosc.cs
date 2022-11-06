using MessagePack;
using Microsoft.Build.Framework;

namespace Hostele.Models;

public class Aktywnosc
{
    public int Id { get; set; }
    public string User { get; set; }
    public DateTime CzasAktywnosci { get; set; }
    public string OpisAktywnosci { get; set; }
}