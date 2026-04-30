using Moq;
using NUnit.Framework;
using Restaurant.Application.Commands;
using Restaurant.Application.Commands.Handlers;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Tests.Commands.Handlers
{
    [TestFixture]
    public class CreateCategoryCommandHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<ICategoryRepository> _categoryRepositoryMock;
        private CreateCategoryCommandHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _unitOfWorkMock.Setup(u => u.CategoryRepository).Returns(_categoryRepositoryMock.Object);
            _handler = new CreateCategoryCommandHandler(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task Handle_ValidCommand_ReturnsCategoryId()
        {
            // Arrange
            var command = new CreateCategoryCommand { Name = "Appetizers" };
            _unitOfWorkMock.Setup(u => u.SaveChangesAsync()).ReturnsAsync(1);

            // Act
            var result = await _handler.Handle(command, CancellationToken.None);

            // Assert
            _categoryRepositoryMock.Verify(r => r.AddAsync(It.Is<Category>(c => c.Name == "Appetizers")), Times.Once);
            _unitOfWorkMock.Verify(u => u.SaveChangesAsync(), Times.Once);
        }
    }
}