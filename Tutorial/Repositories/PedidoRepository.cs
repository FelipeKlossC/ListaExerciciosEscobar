using Microsoft.EntityFrameworkCore;
using Tutorial.Data;
using Tutorial.Models;

namespace Tutorial.Repositories
{
    public class PedidoRepository
    {
        private readonly AppDbContext _context = new AppDbContext();

        public void Adicionar(Pedido pedido)
        {
            _context.Pedidos.Add(pedido);
            _context.SaveChanges();
        }

        public List<Pedido> ListarComItens() =>
            _context.Pedidos.Include(p => p.Itens).ToList();

        // Desafio: total de itens do pedido
        public int TotalItens(int pedidoId)
        {
            var pedido = _context.Pedidos.Include(p => p.Itens)
                                         .FirstOrDefault(p => p.Id == pedidoId);
            return pedido?.Itens.Sum(i => i.Quantidade) ?? 0;
        }
    }
}