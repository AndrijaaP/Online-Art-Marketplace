using System;
using System.Collections.Generic;

namespace UmetnickaDelaProjekat1.Models;

public partial class StavkePorudzbine
{
    public int Id { get; set; }

    public int PorudzbinaId { get; set; }

    public int UmetnickoDeloId { get; set; }

    public int Kolicina { get; set; }

    public decimal CenaPoJedinici { get; set; }

    public virtual Porudzbine Porudzbina { get; set; } = null!;

    public virtual UmetnickaDela UmetnickoDelo { get; set; } = null!;
}
