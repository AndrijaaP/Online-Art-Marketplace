using System;
using System.Collections.Generic;

namespace UmetnickaDelaProjekat1.Models;

public partial class UmetnickaDela
{
    public int Id { get; set; }

    public string Naziv { get; set; } = null!;

    public string? Opis { get; set; }

    public string Umetnik { get; set; } = null!;

    public string Kategorija { get; set; } = null!;

    public decimal Cena { get; set; }

    public string? Fotografija { get; set; }

    public bool Dostupnost { get; set; }

    public virtual ICollection<StavkePorudzbine> StavkePorudzbines { get; set; } = new List<StavkePorudzbine>();
}
