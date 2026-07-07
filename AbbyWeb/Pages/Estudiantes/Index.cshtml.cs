using AbbyWeb.Data;
using AbbyWeb.Models;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AbbyWeb.Pages.Estudiantes
{
    public class IndexModel : PageModel
    {
        private readonly ApplicationDbContext _db;
        public List<Estudiante> Estudiantes { get; set; } = new();

        public IndexModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            try
            {
                Estudiantes = _db.Estudiantes.ToList();
            }
            catch (Exception)
            {
                TempData["error"] = "Error al cargar la lista de estudiantes.";
            }
        }
    }
}
