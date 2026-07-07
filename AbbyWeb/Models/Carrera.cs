using System.ComponentModel.DataAnnotations;

namespace AbbyWeb.Models
{
    public class Carrera
    {
        [Key]
        public int id_carrera { get; set; }

        [StringLength(20)]
        public string clave { get; set; }

        [StringLength(200)]
        public string nombre { get; set; }

        public int duracion_semestres { get; set; }

        [StringLength(10)]
        public string? status { get; set; }
    }
}
