using System.ComponentModel.DataAnnotations;

namespace Estudiantes.Backend;

public class Estudiante
{
    public Guid Id { get; set; }
    public required string Nombre { get; set; }
    public required string Documento { get; set; }
    public required string Edad { get; set; }
    public required string Genero { get; set; }
    public required  string Telefono { get; set; }
    public required string Email { get; set; }
    public required string Curso { get; set; }
}
public class CrearActualizarEstudiante
{
    [Required(ErrorMessage = "El nombre es obligatorio")]
    [StringLength(100, MinimumLength = 15, ErrorMessage = "El nombre debe tener entre 15 y 100 caracteres")]
    public required string Nombre { get; set; }

    [Required(ErrorMessage = "El documento es obligatorio")]
    [RegularExpression("^[0-9]+$", ErrorMessage = "El documento debe ser un número")]
    public required string Documento { get; set; }

    [Required(ErrorMessage = "La edad es obligatoria")]
    [Range(1, 120, ErrorMessage = "La edad debe estar entre 1 y 120 años")]
    public required string Edad { get; set; }

    [Required(ErrorMessage = "El género es obligatorio")]
    public required string Genero { get; set; }

    [Required(ErrorMessage = "El teléfono es obligatorio")]
    [Phone(ErrorMessage = "El formato del teléfono no es válido")]
    public required string Telefono { get; set; }

    [Required(ErrorMessage = "El email es obligatorio")]
    [EmailAddress(ErrorMessage = "El formato del email no es válido")]
    public required string Email { get; set; }

    [Required(ErrorMessage = "El curso es obligatorio")]
    public required string Curso { get; set; }
}
