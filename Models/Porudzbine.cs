using System;
using System.Collections.Generic;

namespace UmetnickaDelaProjekat1.Models;

public partial class Porudzbine
{
    public int Id { get; set; }

    public int KorisnikId { get; set; }

    public DateTime? DatumPorudzbine { get; set; }

    public string Status { get; set; } = null!;

    public virtual Korisnici Korisnik { get; set; } = null!;

    public virtual ICollection<StavkePorudzbine> StavkePorudzbines { get; set; } = new List<StavkePorudzbine>();
}
