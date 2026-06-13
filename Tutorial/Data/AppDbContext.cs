using Microsoft.EntityFrameworkCore;
using Tutorial.Models;

namespace Tutorial.Data
{
    public class AppDbContext : DbContext
    {
        public DbSet<Estudante>   Estudantes   { get; set; }
        public DbSet<Produto>     Produtos     { get; set; }
        public DbSet<Cliente>     Clientes     { get; set; }
        public DbSet<Curso>       Cursos       { get; set; }
        public DbSet<ItemEstoque> Estoque      { get; set; }
        public DbSet<Professor>   Professores  { get; set; }
        public DbSet<Pedido>      Pedidos      { get; set; }
        public DbSet<ItemPedido>  ItensPedido  { get; set; }

        protected override void OnConfiguring(DbContextOptionsBuilder options)
            => options.UseMySql(
                "server=localhost;database=escola;user=root;password=positivo",
                ServerVersion.AutoDetect("server=localhost;database=escola;user=root;password=positivo"));
    }
}