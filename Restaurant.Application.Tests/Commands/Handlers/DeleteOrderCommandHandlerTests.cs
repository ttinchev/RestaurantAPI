using Moq;
using NUnit.Framework;
using Restaurant.Application.Commands;
using Restaurant.Application.Commands.Handlers;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Tests.Commands.Handlers
{
    [TestFixture]
    public class DeleteOrderCommandHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<IOrderRepository> _orderRepositoryMock;
        private DeleteOrderCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _orderRepositoryMock = new Mock<IOrderRepository>();
            _unitOfWorkMock.Setup(u => u.OrdersRepository).Returns(_orderRepositoryMock.Object);
            _handler = new DeleteOrderCommandHandler(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task Handle_ExistingOrder_DeletesAndReturnsTrue()
        {
            // Arrange
            var existingOrder = new Order { Id = 1, TableId = 1 };
            var command = new DeleteOrderCommand(1);

            _orderRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingOrder);
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.True);
            _orderRepositoryMock.Verify(r => r.Delete(existingOrder), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }

        [Test]
        public async Task Handle_NonExistingOrder_ReturnsFalse()
        {
            // Arrange
            var command = new DeleteOrderCommand(999);
            _orderRepositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Order)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.False);
            _orderRepositoryMock.Verify(r => r.Delete(It.IsAny<Order>()), Times.Never);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Never);
        }
    }
}