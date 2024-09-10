using Microsoft.AspNetCore.Http.HttpResults;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;

namespace Estudiantes.Backend.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class EstudiantesController(EstudiantesDbContext context) : ControllerBase
    {
        private readonly EstudiantesDbContext _context = context;
        [HttpGet]
        public async Task<ActionResult<IEnumerable<Estudiante>>> GetAll()
        {
            var estudiantes = await _context.Estudiantes.ToListAsync();
            if (estudiantes.Any())
            {
                return Ok(new Response<IEnumerable<Estudiante>>
                {
                    IsSuccess = true,
                    Result = estudiantes,
                    Message = "Listado De Estudiantes"
                });
            }
            return Ok(new Response<IEnumerable<Estudiante>>
            {
                IsSuccess = false,
                Message = "No hay estudiantes",
                Result = [],
            });
        }

        [HttpPost]
        public async Task<IActionResult> Post([FromBody] CrearActualizarEstudiante estudiante)
        {
            if (!ModelState.IsValid)
            {
                return BadRequest(new Response<CrearActualizarEstudiante> { IsSuccess = false, Message = "Modelo Invalido", Result = estudiante }); // <--- Error 400>
            }
            var nuevoEstudiante = new Estudiante
            {
                Nombre = estudiante.Nombre,
                Documento = estudiante.Documento,
                Edad = estudiante.Edad,
                Genero = estudiante.Genero,
                Telefono = estudiante.Telefono,
                Email = estudiante.Email,
                Curso = estudiante.Curso
            };
            await _context.Estudiantes.AddAsync(nuevoEstudiante);
            await _context.SaveChangesAsync();
            return Ok(new Response<Estudiante>
            {
                IsSuccess = true,
                Message = "Estudiante Creado",
                Result = nuevoEstudiante
            });

        }

        [HttpGet("{id}")]
        public async Task<ActionResult<Estudiante>> GetById(Guid id)
        {

            if (id == Guid.Empty)
            {
                return NotFound(new Response<Estudiante>
                {
                    IsSuccess = false,
                    Message = "Estudiante No Encontrado",
                    Result = null
                });
            }
            var estudiante = await GetEstudiante(id);
            if (estudiante != null)
            {
                return Ok(new Response<Estudiante>
                {
                    IsSuccess = true,
                    Message = "Estudiante Encontrado",
                    Result = estudiante
                });
            }
            return NotFound(new Response<Estudiante>
            {
                IsSuccess = false,
                Message = "Estudiante No Encontrado",
                Result = null
            });
        }

        [HttpDelete("{id}")]
        public async Task<IActionResult> Delete(Guid id)
        {
            if (id == Guid.Empty)
            {
                return BadRequest(new Response<Estudiante>
                {
                    IsSuccess = false,
                    Message = "Id Invalido",
                    Result = null
                });
            }
            var estudiante = await GetEstudiante(id);
            if (estudiante !=null)
            {
                _context.Estudiantes.Remove(estudiante);
                await _context.SaveChangesAsync();
                return Ok(new Response<Estudiante>
                {
                    IsSuccess = true,
                    Message = $"Estudiante Eliminado{estudiante.Nombre}",
                    Result = estudiante
                });
            }
            return NotFound(new Response<Estudiante>
            {
                IsSuccess = false,
                Message = "Estudiante No Encontrado",
                Result = estudiante
            });
        }


        [HttpPut("{id}")]
        public async Task<IActionResult> Put(string id, [FromBody] CrearActualizarEstudiante estudiante)
        {
            if (string.IsNullOrEmpty(id))
            {
                return BadRequest(new Response<CrearActualizarEstudiante> { IsSuccess = false, Message = "Id Inválido", Result = estudiante });
            }

            if (!ModelState.IsValid)
            {
                return BadRequest(new Response<CrearActualizarEstudiante> { IsSuccess = false, Message = "Modelo Inválido", Result = estudiante });
            }

            var estudianteExistente = await GetEstudiante(Guid.Parse(id));
            if (estudianteExistente != null)
            {
                estudianteExistente.Nombre = estudiante.Nombre;
                estudianteExistente.Documento = estudiante.Documento;
                estudianteExistente.Edad = estudiante.Edad;
                estudianteExistente.Genero = estudiante.Genero;
                estudianteExistente.Telefono = estudiante.Telefono;
                estudianteExistente.Email = estudiante.Email;
                estudianteExistente.Curso = estudiante.Curso;

                _context.Estudiantes.Update(estudianteExistente);
                await _context.SaveChangesAsync();

                return Ok(new Response<Estudiante>
                {
                    IsSuccess = true,
                    Message = "Estudiante Actualizado",
                    Result = estudianteExistente
                });
            }

            return NotFound(new Response<Estudiante>
            {
                IsSuccess = false,
                Message = "Estudiante No Encontrado",
                Result = null
            });
        }

        private async Task<Estudiante?> GetEstudiante(Guid id)
        {
            return await _context.Estudiantes.FirstOrDefaultAsync(x => x.Id == id);
        }
    }

   

}




