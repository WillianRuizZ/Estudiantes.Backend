using Microsoft.EntityFrameworkCore;

namespace Estudiantes.Backend
{
    public class EstudiantesDbContext(DbContextOptions options) : DbContext(options)
    {
        public DbSet  <Estudiante> Estudiantes { get; set; } 
    }
}
