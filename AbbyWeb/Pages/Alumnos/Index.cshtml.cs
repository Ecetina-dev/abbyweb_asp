using AbbyWeb.Data;
using AbbyWeb.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AbbyWeb.Pages.Alumnos
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public List<Alumno> Alumnos { get; set; } = new();

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            try
            {
                Alumnos = _db.Alumnos
                    .Include(a => a.Carrera)
                    .Where(a => a.is_deleted == null || a.is_deleted == 0)
                    .ToList();
            }
            catch (Exception)
            {
                TempData["error"] = "Error al cargar la lista de alumnos. Verifique la conexión con la base de datos.";
            }
        }
    }
}
