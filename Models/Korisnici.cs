using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

namespace UmetnickaDelaProjekat1.Models;

public partial class Korisnici
{
    public int Id { get; set; }

    public string Ime { get; set; } = null!;

    public string Prezime { get; set; } = null!;

    public string Email { get; set; } = null!;

    public string LozinkaHash { get; set; } = null!;

    public string TipKorisnika { get; set; } = null!;

    public virtual ICollection<Porudzbine> Porudzbines { get; set; } = new List<Porudzbine>();
}


