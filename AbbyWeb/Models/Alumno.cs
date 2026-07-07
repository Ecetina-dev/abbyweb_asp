using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace AbbyWeb.Models
{
    public class Alumno
    {
        [Key]
        public int id_alumno { get; set; }

        [Required(ErrorMessage = "El numero de control es obligatorio")]
        [Display(Name = "No. Control")]
        [StringLength(15)]
        [RegularExpression(@"^[a-zA-Z0-9\-]+$", ErrorMessage = "El No. Control solo puede contener letras, numeros y guiones.")]
        public string no_control { get; set; }

        [Required(ErrorMessage = "El nombre es obligatorio")]
        [Display(Name = "Nombre")]
        [StringLength(100)]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]+$", ErrorMessage = "El nombre solo puede contener letras y espacios.")]
        public string nombre { get; set; }

        [Required(ErrorMessage = "El apellido paterno es obligatorio")]
        [Display(Name = "Apellido Paterno")]
        [StringLength(80)]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]+$", ErrorMessage = "El apellido paterno solo puede contener letras y espacios.")]
        public string apellido_paterno { get; set; }

        [Display(Name = "Apellido Materno")]
        [StringLength(80)]
        [RegularExpression(@"^[a-zA-ZáéíóúÁÉÍÓÚüÜñÑ\s]*$", ErrorMessage = "El apellido materno solo puede contener letras y espacios.")]
        public string? apellido_materno { get; set; }

        [Display(Name = "Email")]
        [StringLength(120)]
        [EmailAddress(ErrorMessage = "Email no valido")]
        public string? email { get; set; }

        [Display(Name = "Telefono")]
        [StringLength(15)]
        [RegularExpression(@"^[0-9\s\-\+()]*$", ErrorMessage = "El telefono solo puede contener numeros, espacios y los simbolos + - ( )")]
        public string? telefono { get; set; }

        [Display(Name = "Genero")]
        [StringLength(30)]
        public string? genero { get; set; }

        [Display(Name = "Fecha de Nacimiento")]
        [DataType(DataType.Date)]
        public DateTime? fecha_nacimiento { get; set; }

        [Display(Name = "Carrera")]
        public int? id_carrera { get; set; }

        [ForeignKey("id_carrera")]
        public virtual Carrera? Carrera { get; set; }

        [Display(Name = "Semestre")]
        [Range(1, 12, ErrorMessage = "El semestre debe estar entre 1 y 12")]
        public int? semestre { get; set; }

        [Display(Name = "Grupo")]
        [StringLength(20)]
        public string? grupo { get; set; }

        [Display(Name = "Turno")]
        [StringLength(20)]
        public string? turno { get; set; }

        [Display(Name = "Status")]
        [StringLength(20)]
        public string status_alumno { get; set; } = "activo";

        public short? is_deleted { get; set; } = 0;
        public DateTime? deleted_at { get; set; }
        public int? deleted_by { get; set; }

        public DateTime? created_at { get; set; }
        public DateTime updated_at { get; set; } = DateTime.Now;
    }
}
