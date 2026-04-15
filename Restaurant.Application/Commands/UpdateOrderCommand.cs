namespace Restaurant.Application.Commands
{
    public class UpdateOrderCommand : CreateOrderCommand
    {
        public int Id { get; set; }
    }
}
