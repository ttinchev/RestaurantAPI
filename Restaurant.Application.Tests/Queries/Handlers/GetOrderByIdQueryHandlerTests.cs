using Moq;
using NUnit.Framework;
using Restaurant.Application.Queries;
using Restaurant.Application.Queries.Handlers;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Tests.Queries.Handlers
{
    [TestFixture]
    public class GetOrderByIdQueryHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IOrderRepository> _orderRepositoryMock;
        private GetOrderByIdQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _unitOfWorkMock.Setup(u => u.OrdersRepository).Returns(_orderRepositoryMock.Object);
            _handler = new GetOrderByIdQueryHandler(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task Handle_ExistingOrder_ReturnsOrderResponseModel()
        {
            // Arrange
            var order = new Order
            {
                Id = 1,
                TableId = 5,
                OrderItems = new List<OrderItem>
                {
                    new OrderItem
                    {
                        MenuItemId = 10,
                        Quantity = 2,
                        MenuItem = new MenuItem { Id = 10, Name = "Pizza", Price = 12.99m }
                    }
                }
            };

            var query = new GetOrderByIdQuery(1);
            _orderRepositoryMock.Setup(r => r.GetOrderWithMenuItemsAsync(1)).ReturnsAsync(order);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.TableId, Is.EqualTo(5));
            Assert.That(result.MenuItems, Has.Count.EqualTo(1));
            Assert.That(result.MenuItems.First().Name, Is.EqualTo("Pizza"));
        }

        [Test]
        public async Task Handle_NonExistingOrder_ReturnsEmptyModel()
        {
            // Arrange
            var query = new GetOrderByIdQuery(999);
            _orderRepositoryMock.Setup(r => r.GetOrderWithMenuItemsAsync(999)).ReturnsAsync((Order)null);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(0));
        }
    }
}