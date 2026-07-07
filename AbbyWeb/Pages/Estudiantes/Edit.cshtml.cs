using AbbyWeb.Data;
using AbbyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.EntityFrameworkCore;
using System.ComponentModel.DataAnnotations;

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

        public IActionResult OnPost()
        {
            SanitizeDatos();

            var validationContext = new ValidationContext(Estudiante);
            var validationResults = new List<ValidationResult>();
            bool isValid = Validator.TryValidateObject(Estudiante, validationContext, validationResults, validateAllProperties: true);

            if (!isValid)
            {
                foreach (var vr in validationResults)
                {
                    ModelState.AddModelError(vr.MemberNames.FirstOrDefault() ?? "", vr.ErrorMessage ?? "");
                }
            }

            if (!ModelState.IsValid)
            {
                return Page();
            }

            try
            {
                var estudianteExistente = _db.Estudiantes.AsNoTracking().FirstOrDefault(e => e.Id_Estudiante == Estudiante.Id_Estudiante);
                if (estudianteExistente == null)
                {
                    TempData["error"] = "Estudiante no encontrado.";
                    return RedirectToPage("Index");
                }

                _db.Estudiantes.Update(Estudiante);
                _db.SaveChanges();
                TempData["success"] = "Estudiante actualizado exitosamente.";
                return RedirectToPage("Index");
            }
            catch (Exception)
            {
                TempData["error"] = "Error al actualizar el estudiante.";
                return Page();
            }
        }

        private void SanitizeDatos()
        {
            if (Estudiante.Nombres != null)
                Estudiante.Nombres = Estudiante.Nombres.Trim();

            if (Estudiante.Apellidos != null)
                Estudiante.Apellidos = Estudiante.Apellidos.Trim();

            if (Estudiante.Direccion != null)
                Estudiante.Direccion = string.IsNullOrWhiteSpace(Estudiante.Direccion) ? null : Estudiante.Direccion.Trim();

            if (Estudiante.Universidad != null)
                Estudiante.Universidad = string.IsNullOrWhiteSpace(Estudiante.Universidad) ? null : Estudiante.Universidad.Trim();

            if (Estudiante.Telefono != null)
                Estudiante.Telefono = string.IsNullOrWhiteSpace(Estudiante.Telefono) ? null : Estudiante.Telefono.Trim();

            if (Estudiante.Correo != null)
                Estudiante.Correo = string.IsNullOrWhiteSpace(Estudiante.Correo) ? null : Estudiante.Correo.Trim();
        }
    }
}
