using AbbyWeb.Data;
using AbbyWeb.Models;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using Microsoft.AspNetCore.Mvc.Rendering;
using System.ComponentModel.DataAnnotations;

namespace AbbyWeb.Pages.Alumnos
{
    public class EditModel : PageModel
    {
        private readonly ApplicationDbContext _db;

        [BindProperty]
        public Alumno Alumno { get; set; }

        public SelectList Carreras { get; set; }

        public EditModel(ApplicationDbContext db)
        {
            _db = db;
        }

        public IActionResult OnGet(int id)
        {
            try
            {
                Alumno = _db.Alumnos.Find(id);
                if (Alumno == null)
                {
                    TempData["error"] = "Alumno no encontrado.";
                    return RedirectToPage("Index");
                }

                Carreras = new SelectList(_db.Carreras, "id_carrera", "nombre", Alumno.id_carrera);
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
                // Sanitización: limpiar espacios y prevenir inyección
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

                // Revalidar modelo completo
                var context = new ValidationContext(Alumno, null, null);
                var results = new List<ValidationResult>();
                if (!Validator.TryValidateObject(Alumno, context, results, true))
                {
                    foreach (var result in results)
                    {
                        foreach (var memberName in result.MemberNames)
                        {
                            ModelState.AddModelError($"Alumno.{memberName}", result.ErrorMessage ?? "");
                        }
                    }
                    Carreras = new SelectList(_db.Carreras, "id_carrera", "nombre", Alumno.id_carrera);
                    return Page();
                }

                var alumnoFromDb = _db.Alumnos.Find(Alumno.id_alumno);
                if (alumnoFromDb == null)
                {
                    TempData["error"] = "Alumno no encontrado.";
                    return RedirectToPage("Index");
                }

                bool existe = _db.Alumnos.Any(a => a.no_control == Alumno.no_control && a.id_alumno != Alumno.id_alumno);
                if (existe)
                {
                    ModelState.AddModelError("Alumno.no_control", "El número de control ya existe.");
                    Carreras = new SelectList(_db.Carreras, "id_carrera", "nombre", Alumno.id_carrera);
                    return Page();
                }

                alumnoFromDb.no_control = Alumno.no_control;
                alumnoFromDb.nombre = Alumno.nombre;
                alumnoFromDb.apellido_paterno = Alumno.apellido_paterno;
                alumnoFromDb.apellido_materno = Alumno.apellido_materno;
                alumnoFromDb.email = Alumno.email;
                alumnoFromDb.telefono = Alumno.telefono;
                alumnoFromDb.genero = Alumno.genero;
                alumnoFromDb.fecha_nacimiento = Alumno.fecha_nacimiento;
                alumnoFromDb.id_carrera = Alumno.id_carrera;
                alumnoFromDb.semestre = Alumno.semestre;
                alumnoFromDb.grupo = Alumno.grupo;
                alumnoFromDb.turno = Alumno.turno;
                alumnoFromDb.updated_at = DateTime.Now;

                await _db.SaveChangesAsync();
                TempData["success"] = "Alumno actualizado exitosamente.";
                return RedirectToPage("Index");
            }
            catch (Exception)
            {
                TempData["error"] = "Ocurrió un error al guardar los cambios. Verifique los datos e intente nuevamente.";
                return RedirectToPage("Index");
            }
        }
    }
}
