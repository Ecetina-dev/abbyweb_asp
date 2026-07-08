using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbbyWeb.Models
{
    public class Estudiante
    {
        [Key]
        [DatabaseGenerated(DatabaseGeneratedOption.Identity)]
        public int Id_Estudiante { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Nombres")]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]+$", ErrorMessage = "Los nombres solo pueden contener letras y espacios.")]
        public string Nombres { get; set; }

        [Required(ErrorMessage = "Los apellidos son obligatorios")]
        [Display(Name = "Apellidos")]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]+$", ErrorMessage = "Los apellidos solo pueden contener letras y espacios.")]
        public string Apellidos { get; set; }

        [Display(Name = "Dirección")]
        [StringLength(200)]
        public string? Direccion { get; set; }

        [Display(Name = "Universidad")]
        [StringLength(150)]
        public string? Universidad { get; set; }

        [Display(Name = "Teléfono")]
        [StringLength(10)]
        [RegularExpression(@"^[0-9]*$", ErrorMessage = "El telefono solo puede contener numeros.")]
        public string? Telefono { get; set; }

        [Display(Name = "Correo")]
        [StringLength(150)]
        [EmailAddress(ErrorMessage = "Correo electronico no valido")]
        public string? Correo { get; set; }

        [Display(Name = "Semestre")]
        [Range(1, 12, ErrorMessage = "El semestre debe estar entre 1 y 12")]
        public int? Semestre { get; set; }

        [Display(Name = "Foto")]
        public byte[]? Foto { get; set; }
    }
}
