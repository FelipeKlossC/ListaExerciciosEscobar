using Tutorial.Data;
using Tutorial.Models;

namespace Tutorial.Repositories
{
    public class CursoRepository
    {
        private readonly AppDbContext _context = new AppDbContext();

        public void Adicionar(Curso c)
        {
            _context.Cursos.Add(c);
            _context.SaveChanges();
        }

        public List<Curso> Listar() => _context.Cursos.ToList();

        public List<Curso> FiltrarPorCargaHoraria(int minima) =>
            _context.Cursos.Where(c => c.CargaHoraria >= minima).ToList();

        public List<Curso> ListarOrdenadoPorNome() =>
            _context.Cursos.OrderBy(c => c.Nome).ToList();
    }
}