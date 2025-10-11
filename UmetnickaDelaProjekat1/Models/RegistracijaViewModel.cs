using System.ComponentModel.DataAnnotations;

namespace UmetnickaDelaProjekat1.Models
{
        public class RegistracijaViewModel
        {
            [Required]
            public string Ime { get; set; }

            [Required]
            public string Prezime { get; set; }

            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Lozinka { get; set; }

            [Display(Name = "Potvrda lozinke")]
            [Required]
            [DataType(DataType.Password)]
            [Compare("Lozinka", ErrorMessage = "Lozinke se ne poklapaju.")]
            public string PotvrdaLozinke { get; set; }

            [Required]
            public string TipKorisnika { get; set; } 
        }
    }

