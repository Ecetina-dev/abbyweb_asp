using AbbyWeb.Data;
using AbbyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Estudiantes
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Estudiante Estudiante { get; set; } = new();

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult OnGet(int id)
        {
            try
            {
                var estudiante = _db.Estudiantes.Find(id);
                if (estudiante == null)
                {
                    TempData["error"] = "Estudiante no encontrado.";
                    return RedirectToPage("Index");
                }

                Estudiante = estudiante;
                return Page();
            }
            catch (Exception)
            {
                TempData["error"] = "Error al cargar el estudiante.";
                return RedirectToPage("Index");
            }
        }

        public IActionResult OnPost()
        {
            try
            {
                var estudiante = _db.Estudiantes.Find(Estudiante.Id_Estudiante);
                if (estudiante == null)
                {
                    TempData["error"] = "Estudiante no encontrado.";
                    return RedirectToPage("Index");
                }

                _db.Estudiantes.Remove(estudiante);
                _db.SaveChanges();
                TempData["success"] = "Estudiante eliminado exitosamente.";
                return RedirectToPage("Index");
            }
            catch (Exception)
            {
                TempData["error"] = "Error al eliminar el estudiante.";
                return Page();
            }
        }
    }
}
