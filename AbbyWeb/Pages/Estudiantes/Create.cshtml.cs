using AbbyWeb.Data;
using AbbyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using System.ComponentModel.DataAnnotations;

namespace AbbyWeb.Pages.Estudiantes
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Estudiante Estudiante { get; set; } = new();

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            Estudiante = new();
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
                _db.Estudiantes.Add(Estudiante);
                _db.SaveChanges();
                TempData["success"] = "Estudiante creado exitosamente.";
                return RedirectToPage("Index");
            }
            catch (Exception)
            {
                TempData["error"] = "Error al crear el estudiante.";
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
