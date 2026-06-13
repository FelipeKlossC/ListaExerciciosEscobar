using Microsoft.EntityFrameworkCore;
using Tutorial.Data;
using Tutorial.Models;

namespace Tutorial.Repositories
{
    public class ProfessorRepository
    {
        private readonly AppDbContext _context = new AppDbContext();

        public void Adicionar(Professor professor)
        {
            _context.Professores.Add(professor);
            _context.SaveChanges();
        }

        public List<Professor> ListarComCursos() =>
            _context.Professores.Include(p => p.Cursos).ToList();

        public Professor BuscarPorIdComCursos(int id) =>
            _context.Professores.Include(p => p.Cursos)
                                 .FirstOrDefault(p => p.Id == id);
    }
}