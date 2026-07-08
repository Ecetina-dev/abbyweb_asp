using AbbyWeb.Data;
using AbbyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;

namespace AbbyWeb.Pages.Estudiantes
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Estudiante Estudiante { get; set; } = new();

        public EditModel(ApplicationDbContext db)
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

        public async Task<IActionResult> OnPost()
        {
            SanitizarDatos();

            if (!ModelState.IsValid)
                return Page();

            try
            {
                var estudianteExistente = _db.Estudiantes.AsNoTracking().FirstOrDefault(e => e.Id_Estudiante == Estudiante.Id_Estudiante);
                if (estudianteExistente == null)
                {
                    TempData["error"] = "Estudiante no encontrado.";
                    return RedirectToPage("Index");
                }

                _db.Estudiantes.Update(Estudiante);
                await _db.SaveChangesAsync();
                TempData["success"] = "Estudiante actualizado exitosamente.";
                return RedirectToPage("Index");
            }
            catch (Exception)
            {
                TempData["error"] = "Error al actualizar el estudiante.";
                return Page();
            }
        }

        private void SanitizarDatos()
        {
            Estudiante.Nombres = Estudiante.Nombres?.Trim() ?? "";
            Estudiante.Apellidos = Estudiante.Apellidos?.Trim() ?? "";
            Estudiante.Direccion = string.IsNullOrWhiteSpace(Estudiante.Direccion) ? null : Estudiante.Direccion.Trim();
            Estudiante.Universidad = string.IsNullOrWhiteSpace(Estudiante.Universidad) ? null : Estudiante.Universidad.Trim();
            Estudiante.Telefono = string.IsNullOrWhiteSpace(Estudiante.Telefono) ? null : Estudiante.Telefono.Trim();
            Estudiante.Correo = string.IsNullOrWhiteSpace(Estudiante.Correo) ? null : Estudiante.Correo.Trim();
        }
    }
}
