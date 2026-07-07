using AbbyWeb.Data;
using AbbyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AbbyWeb.Pages.Alumnos
{
    public class DeleteModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Alumno Alumno { get; set; }

        public string CarreraNombre { get; set; }

        public DeleteModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult OnGet(int id)
        {
            try
            {
                Alumno = _db.Alumnos.Include(a => a.Carrera).FirstOrDefault(a => a.id_alumno == id);
                if (Alumno == null)
                {
                    TempData["error"] = "Alumno no encontrado.";
                    return RedirectToPage("Index");
                }

                CarreraNombre = Alumno.Carrera?.nombre ?? "Sin carrera";
                return Page();
            }
            catch (Exception)
            {
                TempData["error"] = "Error al cargar los datos. Verifique la conexión con la base de datos.";
                return RedirectToPage("Index");
            }
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                var alumnoFromDb = _db.Alumnos.Find(Alumno.id_alumno);
                if (alumnoFromDb == null)
                {
                    TempData["error"] = "Alumno no encontrado.";
                    return RedirectToPage("Index");
                }

                alumnoFromDb.is_deleted = 1;
                alumnoFromDb.deleted_at = DateTime.Now;
                alumnoFromDb.updated_at = DateTime.Now;
                await _db.SaveChangesAsync();
                TempData["success"] = "Alumno eliminado exitosamente.";
                return RedirectToPage("Index");
            }
            catch (Exception)
            {
                TempData["error"] = "Ocurrió un error al eliminar el alumno. Puede tener registros relacionados que impiden su eliminación.";
                return RedirectToPage("Index");
            }
        }
    }
}
