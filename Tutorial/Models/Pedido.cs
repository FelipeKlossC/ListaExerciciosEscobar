namespace Tutorial.Models
{
    public class Pedido
    {
        public int Id { get; set; }
        public DateTime Data { get; set; }
        public List<ItemPedido> Itens { get; set; } = new List<ItemPedido>();
    }
}