using Moq;
using NUnit.Framework;
using Restaurant.Application.Commands;
using Restaurant.Application.Commands.Handlers;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Tests.Commands.Handlers
{
    [TestFixture]
    public class UpdateCategoryCommandHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<ICategoryRepository> _categoryRepositoryMock;
        private UpdateCategoryCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _unitOfWorkMock.Setup(u => u.CategoryRepository).Returns(_categoryRepositoryMock.Object);
            _handler = new UpdateCategoryCommandHandler(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task Handle_ExistingCategory_UpdatesAndReturnsId()
        {
            // Arrange
            var existingCategory = new Category { Id = 1, Name = "Old Name" };
            var command = new UpdateCategoryCommand { Id = 1, Name = "New Name" };

            _categoryRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(existingCategory);
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            Assert.That(result, Is.EqualTo(1));
            Assert.That(existingCategory.Name, Is.EqualTo("New Name"));
            _categoryRepositoryMock.Verify(r => r.Update(existingCategory), Times.Once);
        }

        [Test]
        public void Handle_NonExistingCategory_ThrowsException()
        {
            // Arrange
            var command = new UpdateCategoryCommand { Id = 999, Name = "Test" };
            _categoryRepositoryMock.Setup(r => r.GetByIdAsync(999)).ReturnsAsync((Category)null);

            // Act & Assert
            Assert.ThrowsAsync<Exception>(() => _handler.Handle(command, CancellationToken.None));
        }
    }
}