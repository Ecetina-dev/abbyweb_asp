using AbbyWeb.Data;
using AbbyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace AbbyWeb.Pages.Estudiantes
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Estudiante Estudiante { get; set; }

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            Estudiante = new Estudiante();
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                SanitizarDatos();

                if (!ModelState.IsValid)
                    return Page();

                await _db.Estudiantes.AddAsync(Estudiante);
                await _db.SaveChangesAsync();
                TempData["success"] = "Estudiante creado exitosamente";
                return RedirectToPage("Index");
            }
            catch (Exception)
            {
                ModelState.AddModelError(string.Empty, "Ocurrio un error al guardar los datos.");
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
