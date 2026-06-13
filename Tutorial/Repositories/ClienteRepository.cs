using Tutorial.Data;
using Tutorial.Models;

namespace Tutorial.Repositories
{
    public class ClienteRepository
    {
        private readonly AppDbContext _context = new AppDbContext();

        public void Adicionar(Cliente c)
        {
            if (!c.Email.Contains("@") || !c.Email.Contains("."))
            {
                Console.WriteLine($"Email inválido: {c.Email} — cliente não salvo.");
                return;
            }
            _context.Clientes.Add(c);
            _context.SaveChanges();
        }

        public List<Cliente> Listar() => _context.Clientes.ToList();

        public Cliente BuscarPorId(int id) =>
            _context.Clientes.FirstOrDefault(c => c.Id == id);

        public void Atualizar(Cliente cliente)
        {
            var existente = _context.Clientes.Find(cliente.Id);
            if (existente != null)
            {
                existente.Nome  = cliente.Nome;
                existente.Email = cliente.Email;
                _context.SaveChanges();
            }
        }

        public void Remover(int id)
        {
            var cliente = _context.Clientes.Find(id);
            if (cliente != null)
            {
                _context.Clientes.Remove(cliente);
                _context.SaveChanges();
            }
        }

        // Desafio: buscar por email
        public Cliente BuscarPorEmail(string email) =>
            _context.Clientes.FirstOrDefault(c => c.Email == email);
    }
}