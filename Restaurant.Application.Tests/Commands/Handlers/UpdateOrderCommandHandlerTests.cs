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
    public class UpdateOrderCommandHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IOrderRepository> _orderRepositoryMock;
        private UpdateOrderCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _unitOfWorkMock.Setup(u => u.OrdersRepository).Returns(_orderRepositoryMock.Object);
            _handler = new UpdateOrderCommandHandler(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task Handle_ExistingOrder_UpdatesAndReturnsId()
        {
            // Arrange
            var existingOrder = new Order
            {
                Id = 1,
                TableId = 1,
                OrderItems = new List<OrderItem>()
            };

            var command = new UpdateOrderCommand
            {
                Id = 1,
                TableId = 2,
                OrderItems = new List<OrderItemRequestModel>
                {
                    new OrderItemRequestModel { MenuItemId = 5, Quantity = 3 }
                }
            };

            _orderRepositoryMock.Setup(r => r.GetOrderWithMenuItemsAsync(1))
                .ReturnsAsync(existingOrder);
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(1));
            Assert.That(existingOrder.TableId, Is.EqualTo(2));
            _orderRepositoryMock.Verify(r => r.Update(existingOrder), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public void Handle_NonExistingOrder_ThrowsException()
        {
            // Arrange
            var command = new UpdateOrderCommand { Id = 999, TableId = 1, OrderItems = new List<OrderItemRequestModel>() };
            _orderRepositoryMock.Setup(r => r.GetOrderWithMenuItemsAsync(999)).ReturnsAsync((Order)null);

            // Act & Assert
            Assert.ThrowsAsync<Exception>(async () => await _handler.Handle(command, CancellationToken.None));
        }
    }
}