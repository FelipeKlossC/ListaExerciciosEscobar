using Tutorial.Data;
using Tutorial.Models;

namespace Tutorial.Repositories
{
    public class EstoqueRepository
    {
        private readonly AppDbContext _context = new AppDbContext();

        public void Adicionar(ItemEstoque item)
        {
            // Desafio: impede estoque negativo
            if (item.Quantidade < 0)
            {
                Console.WriteLine($"Quantidade negativa para '{item.Nome}'. Item não inserido.");
                return;
            }
            _context.Estoque.Add(item);
            _context.SaveChanges();
        }

        public List<ItemEstoque> Listar() => _context.Estoque.ToList();

        public void DarBaixa(int id, int quantidade)
        {
            var item = _context.Estoque.Find(id);
            if (item == null) { Console.WriteLine("Item não encontrado."); return; }

            if (item.Quantidade - quantidade < 0)
            {
                Console.WriteLine($"Baixa cancelada: '{item.Nome}' ficaria negativo.");
                return;
            }
            item.Quantidade -= quantidade;
            _context.SaveChanges();
            Console.WriteLine($"Baixa de {quantidade} unidade(s) de '{item.Nome}' realizada.");
        }

        public List<ItemEstoque> ListarEstoqueBaixo(int limite = 5) =>
            _context.Estoque.Where(i => i.Quantidade <= limite).OrderBy(i => i.Quantidade).ToList();
    }
}