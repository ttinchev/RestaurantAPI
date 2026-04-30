using Moq;
using NUnit.Framework;
using Restaurant.Application.Queries;
using Restaurant.Application.Queries.Handlers;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Tests.Queries.Handlers
{
    [TestFixture]
    public class GetAllOrdersQueryHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IOrderRepository> _orderRepositoryMock;
        private GetAllOrdersQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _unitOfWorkMock.Setup(u => u.OrdersRepository).Returns(_orderRepositoryMock.Object);
            _handler = new GetAllOrdersQueryHandler(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task Handle_ReturnsAllOrders()
        {
            // Arrange
            var orders = new List<Order>
            {
                new Order
                {
                    Id = 1,
                    TableId = 1,
                    OrderItems = new List<OrderItem>
                    {
                        new OrderItem
                        {
                            MenuItemId = 1,
                            Quantity = 2,
                            MenuItem = new MenuItem { Id = 1, Name = "Burger", Price = 8.99m }
                        }
                    }
                },
                new Order
                {
                    Id = 2,
                    TableId = 2,
                    OrderItems = new List<OrderItem>()
                }
            };

            var query = new GetAllOrdersQuery();
            _orderRepositoryMock.Setup(r => r.GetAllWithMenuItemsAsync()).ReturnsAsync(orders);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
            Assert.That(result.First().Id, Is.EqualTo(1));
        }
    }
}