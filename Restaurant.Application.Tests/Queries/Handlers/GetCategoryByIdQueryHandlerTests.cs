using Moq;
using NUnit.Framework;
using Restaurant.Application.Queries;
using Restaurant.Application.Queries.Handlers;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Tests.Queries.Handlers
{
    [TestFixture]
    public class GetCategoryByIdQueryHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<ICategoryRepository> _categoryRepositoryMock;
        private GetCategoryByIdQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _unitOfWorkMock.Setup(u => u.CategoryRepository).Returns(_categoryRepositoryMock.Object);
            _handler = new GetCategoryByIdQueryHandler(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task Handle_ExistingCategory_ReturnsCategoryResponseModel()
        {
            // Arrange
            var category = new Category { Id = 1, Name = "Desserts" };
            var query = new GetCategoryByIdQuery(1);
            _categoryRepositoryMock.Setup(r => r.GetByIdAsync(1)).ReturnsAsync(category);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Id, Is.EqualTo(1));
            Assert.That(result.Name, Is.EqualTo("Desserts"));
        }
    }
}