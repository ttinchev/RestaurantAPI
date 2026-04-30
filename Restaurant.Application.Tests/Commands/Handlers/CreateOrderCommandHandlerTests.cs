using Moq;
using NUnit.Framework;
using Restaurant.Application.Commands;
using Restaurant.Application.Commands.Handlers;
using Restaurant.Application.Models.Order;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Tests.Commands.Handlers
{
    [TestFixture]
    public class CreateOrderCommandHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IOrderRepository> _orderRepositoryMock;
        private CreateOrderCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _unitOfWorkMock.Setup(u => u.OrdersRepository).Returns(_orderRepositoryMock.Object);
            _handler = new CreateOrderCommandHandler(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task Handle_ValidCommand_ReturnsOrderId()
        {
            // Arrange
            var command = new CreateOrderCommand
            {
                TableId = 1,
                OrderItems = new List<OrderItemRequestModel>
                {
                    new OrderItemRequestModel { MenuItemId = 1, Quantity = 2 },
                    new OrderItemRequestModel { MenuItemId = 2, Quantity = 1 }
                }
            };

            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(1));
            _orderRepositoryMock.Verify(r => r.AddAsync(It.IsAny<Order>()), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task Handle_ValidCommand_CreatesOrderWithCorrectProperties()
        {
            // Arrange
            var command = new CreateOrderCommand
            {
                TableId = 5,
                OrderItems = new List<OrderItemRequestModel>
                {
                    new OrderItemRequestModel { MenuItemId = 3, Quantity = 4 }
                }
            };

            Order capturedOrder = null;
            _orderRepositoryMock.Setup(r => r.AddAsync(It.IsAny<Order>()))
                .Callback<Order>(o => capturedOrder = o);
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(capturedOrder, Is.Not.Null);
            Assert.That(capturedOrder.TableId, Is.EqualTo(5));
            Assert.That(capturedOrder.OrderItems, Has.Count.EqualTo(1));
            Assert.That(capturedOrder.OrderItems.First().MenuItemId, Is.EqualTo(3));
            Assert.That(capturedOrder.OrderItems.First().Quantity, Is.EqualTo(4));
        }
    }
}