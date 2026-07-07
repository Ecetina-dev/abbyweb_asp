using AbbyWeb.Data;
using AbbyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AbbyWeb.Pages.Alumnos
{
    public class CreateModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Alumno Alumno { get; set; }

        public SelectList Carreras { get; set; }

        public CreateModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public void OnGet()
        {
            try
            {
                Carreras = new SelectList(_db.Carreras, "id_carrera", "nombre");
                Alumno = new Alumno();
            }
            catch (Exception)
            {
                TempData["error"] = "Error al cargar los datos. Verifique la conexión con la base de datos.";
            }
        }

        public async Task<IActionResult> OnPost()
        {
            try
            {
                // Sanitización: limpiar espacios y prevenir inyección
                SanitizarDatos();

                // Revalidar el modelo completo después de sanitizar
                var context = new ValidationContext(Alumno, null, null);
                var results = new List<ValidationResult>();
                if (!Validator.TryValidateObject(Alumno, context, results, true))
                {
                    foreach (var result in results)
                    {
                        foreach (var memberName in result.MemberNames)
                        {
                            ModelState.AddModelError($"Alumno.{memberName}", result.ErrorMessage);
                        }
                    }
                    Carreras = new SelectList(_db.Carreras, "id_carrera", "nombre");
                    return Page();
                }

                if (_db.Alumnos.Any(a => a.no_control == Alumno.no_control))
                {
                    ModelState.AddModelError("Alumno.no_control", "El número de control ya existe.");
                    Carreras = new SelectList(_db.Carreras, "id_carrera", "nombre");
                    return Page();
                }

                Alumno.created_at = DateTime.Now;
                Alumno.updated_at = DateTime.Now;

                await _db.Alumnos.AddAsync(Alumno);
                await _db.SaveChangesAsync();
                TempData["success"] = "Alumno creado exitosamente";
                return RedirectToPage("Index");
            }
            catch (Exception)
            {
                Carreras = new SelectList(_db.Carreras, "id_carrera", "nombre");
                ModelState.AddModelError(string.Empty, "Ocurrió un error al guardar los datos. Verifique que todos los campos sean válidos.");
                return Page();
            }
        }

        private void SanitizarDatos()
        {
            Alumno.no_control = Alumno.no_control?.Trim() ?? "";
            Alumno.nombre = Alumno.nombre?.Trim() ?? "";
            Alumno.apellido_paterno = Alumno.apellido_paterno?.Trim() ?? "";
            Alumno.apellido_materno = Alumno.apellido_materno?.Trim();
            if (string.IsNullOrWhiteSpace(Alumno.apellido_materno)) Alumno.apellido_materno = null;
            Alumno.email = Alumno.email?.Trim();
            if (string.IsNullOrWhiteSpace(Alumno.email)) Alumno.email = null;
            Alumno.telefono = Alumno.telefono?.Trim();
            if (string.IsNullOrWhiteSpace(Alumno.telefono)) Alumno.telefono = null;
            Alumno.genero = Alumno.genero?.Trim();
            if (string.IsNullOrWhiteSpace(Alumno.genero)) Alumno.genero = null;
            Alumno.grupo = Alumno.grupo?.Trim();
            if (string.IsNullOrWhiteSpace(Alumno.grupo)) Alumno.grupo = null;
            Alumno.turno = Alumno.turno?.Trim();
            if (string.IsNullOrWhiteSpace(Alumno.turno)) Alumno.turno = null;
        }
    }
}
