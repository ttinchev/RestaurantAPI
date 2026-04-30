using Moq;
using NUnit.Framework;
using Restaurant.Application.Commands;
using Restaurant.Application.Commands.Handlers;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Tests.Commands.Handlers
{
    [TestFixture]
    public class DeleteCategoryCommandHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<ICategoryRepository> _categoryRepositoryMock;
        private DeleteCategoryCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _unitOfWorkMock.Setup(u => u.CategoryRepository).Returns(_categoryRepositoryMock.Object);
            _handler = new DeleteCategoryCommandHandler(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task Handle_ExistingCategory_DeletesAndReturnsTrue()
        {
            // Arrange
            var existingCategory = new Category { Id = 1, Name = "Test" };
            var command = new DeleteCategoryCommand(1);

            _categoryRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingCategory);
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.True);
            _categoryRepositoryMock.Verify(r => r.Delete(existingCategory), Times.Once);
        }

        [Test]
        public async Task Handle_NonExistingCategory_ReturnsFalse()
        {
            // Arrange
            var command = new DeleteCategoryCommand(999);
            _categoryRepositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Category)null);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.False);
            _categoryRepositoryMock.Verify(r => r.Delete(It.IsAny<Category>()), Times.Never);
        }
    }
}