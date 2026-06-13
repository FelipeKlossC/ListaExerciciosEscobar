using Tutorial.Data;
using Tutorial.Models;
using Tutorial.Repositories;

// ── TUTORIAL ────────────────────────────────────────────
Console.WriteLine("===== TUTORIAL: ESTUDANTES =====");
var estudanteRepo = new EstudanteRepository();
estudanteRepo.Adicionar(new Estudante { Nome = "João Silva", Idade = 22 });
foreach (var e in estudanteRepo.Listar())
    Console.WriteLine($"{e.Id} - {e.Nome} - {e.Idade}");
estudanteRepo.Atualizar(new Estudante { Id = 1, Nome = "Maria Oliveira", Idade = 25 });
estudanteRepo.Remover(1);
Console.WriteLine("Lista final:");
foreach (var e in estudanteRepo.Listar())
    Console.WriteLine($"{e.Id} - {e.Nome} - {e.Idade}");

// ── ATIVIDADE 1: PRODUTOS ────────────────────────────────
Console.WriteLine("\n===== ATIVIDADE 1: PRODUTOS =====");
var produtoRepo = new ProdutoRepository();
produtoRepo.Adicionar(new Produto { Nome = "Notebook", Preco = 3500 });
produtoRepo.Adicionar(new Produto { Nome = "Mouse",    Preco = 89.90 });
produtoRepo.Adicionar(new Produto { Nome = "Teclado",  Preco = 149.90 });
foreach (var p in produtoRepo.Listar())
    Console.WriteLine($"{p.Id} - {p.Nome} - R$ {p.Preco:F2}");
Console.WriteLine("Acima de R$ 100:");
foreach (var p in produtoRepo.ListarAcimaDe(100))
    Console.WriteLine($"  {p.Nome} - R$ {p.Preco:F2}");

// ── ATIVIDADE 2: CLIENTES ────────────────────────────────
Console.WriteLine("\n===== ATIVIDADE 2: CLIENTES =====");
var clienteRepo = new ClienteRepository();
clienteRepo.Adicionar(new Cliente { Nome = "Ana Silva",   Email = "ana@email.com" });
clienteRepo.Adicionar(new Cliente { Nome = "Bruno Costa", Email = "emailinvalido" });
foreach (var c in clienteRepo.Listar())
    Console.WriteLine($"{c.Id} - {c.Nome} - {c.Email}");
Console.WriteLine("Busca por email:");
var cli = clienteRepo.BuscarPorEmail("ana@email.com");
Console.WriteLine(cli != null ? $"  Encontrado: {cli.Nome}" : "  Não encontrado.");

// ── ATIVIDADE 3: CURSOS ──────────────────────────────────
Console.WriteLine("\n===== ATIVIDADE 3: CURSOS =====");
var cursoRepo = new CursoRepository();
cursoRepo.Adicionar(new Curso { Nome = "Python",         CargaHoraria = 40 });
cursoRepo.Adicionar(new Curso { Nome = "C#",             CargaHoraria = 60 });
cursoRepo.Adicionar(new Curso { Nome = "Banco de Dados", CargaHoraria = 30 });
cursoRepo.Adicionar(new Curso { Nome = "Java",           CargaHoraria = 80 });
cursoRepo.Adicionar(new Curso { Nome = "HTML e CSS",     CargaHoraria = 20 });
Console.WriteLine("Filtrados >= 40h:");
foreach (var c in cursoRepo.FiltrarPorCargaHoraria(40))
    Console.WriteLine($"  {c.Nome} - {c.CargaHoraria}h");
Console.WriteLine("Ordenados por nome:");
foreach (var c in cursoRepo.ListarOrdenadoPorNome())
    Console.WriteLine($"  {c.Nome}");

// ── ATIVIDADE 4: ESTOQUE ─────────────────────────────────
Console.WriteLine("\n===== ATIVIDADE 4: ESTOQUE =====");
var estoqueRepo = new EstoqueRepository();
estoqueRepo.Adicionar(new ItemEstoque { Nome = "Arroz",   Quantidade = 50 });
estoqueRepo.Adicionar(new ItemEstoque { Nome = "Feijão",  Quantidade = 3  });
estoqueRepo.Adicionar(new ItemEstoque { Nome = "Óleo",    Quantidade = 20 });
estoqueRepo.Adicionar(new ItemEstoque { Nome = "Açúcar",  Quantidade = -5 }); // bloqueado!
estoqueRepo.DarBaixa(1, 10);
estoqueRepo.DarBaixa(2, 10); // bloqueado!
Console.WriteLine("Estoque baixo (<=5):");
foreach (var i in estoqueRepo.ListarEstoqueBaixo(5))
    Console.WriteLine($"  {i.Nome} - {i.Quantidade} un.");

// ── ATIVIDADE 5: PROFESSOR E CURSOS (1:N) ────────────────
Console.WriteLine("\n===== ATIVIDADE 5: PROFESSOR E CURSOS =====");
var profRepo = new ProfessorRepository();
profRepo.Adicionar(new Professor
{
    Nome = "Carlos Silva",
    Cursos = new List<Curso>
    {
        new Curso { Nome = "C# Básico",        CargaHoraria = 40 },
        new Curso { Nome = "Entity Framework",  CargaHoraria = 30 }
    }
});
profRepo.Adicionar(new Professor
{
    Nome = "Fernanda Lima",
    Cursos = new List<Curso>
    {
        new Curso { Nome = "Python Básico",    CargaHoraria = 40 },
        new Curso { Nome = "Data Science",     CargaHoraria = 60 }
    }
});
Console.WriteLine("Professores com cursos (Include):");
foreach (var prof in profRepo.ListarComCursos())
{
    Console.WriteLine($"Professor: {prof.Nome}");
    foreach (var c in prof.Cursos)
        Console.WriteLine($"  └─ {c.Nome} - {c.CargaHoraria}h");
}

// ── ATIVIDADE 6: PEDIDO E ITENS (1:N) ───────────────────
Console.WriteLine("\n===== ATIVIDADE 6: PEDIDO E ITENS =====");
var pedidoRepo = new PedidoRepository();
pedidoRepo.Adicionar(new Pedido
{
    Data = DateTime.Now,
    Itens = new List<ItemPedido>
    {
        new ItemPedido { Produto = "Notebook", Quantidade = 1 },
        new ItemPedido { Produto = "Mouse",    Quantidade = 2 }
    }
});
Console.WriteLine("Pedidos com itens (Include):");
foreach (var pedido in pedidoRepo.ListarComItens())
{
    Console.WriteLine($"Pedido {pedido.Id} - {pedido.Data:dd/MM/yyyy}");
    foreach (var item in pedido.Itens)
        Console.WriteLine($"  └─ {item.Produto} x{item.Quantidade}");

    // Desafio: total de itens
    var total = pedido.Itens.Sum(i => i.Quantidade);
    Console.WriteLine($"  Total de itens: {total}");
}

Console.WriteLine("\n=== FIM DA EXECUÇÃO ===");