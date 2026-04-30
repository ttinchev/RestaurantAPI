using Moq;
using NUnit.Framework;
using Restaurant.Application.Queries;
using Restaurant.Application.Queries.Handlers;
using Restaurant.Domain.Entities;
using Restaurant.Domain.Interfaces;

namespace Restaurant.Application.Tests.Queries.Handlers
{
    [TestFixture]
    public class GetAllCategoriesQueryHandlerTests
    {
        private Mock<IUnitOfWork> _unitOfWorkMock;
        private Mock<ICategoryRepository> _categoryRepositoryMock;
        private GetAllCategoriesQueryHandler _handler;

        [SetUp]
        public void SetUp()
        {
            _unitOfWorkMock = new Mock<IUnitOfWork>();
            _categoryRepositoryMock = new Mock<ICategoryRepository>();
            _unitOfWorkMock.Setup(u => u.CategoryRepository).Returns(_categoryRepositoryMock.Object);
            _handler = new GetAllCategoriesQueryHandler(_unitOfWorkMock.Object);
        }

        [Test]
        public async Task Handle_ReturnsAllCategories()
        {
            // Arrange
            var categories = new List<Category>
            {
                new Category { Id = 1, Name = "Starters" },
                new Category { Id = 2, Name = "Main Course" }
            };

            var query = new GetAllCategoriesQuery();
            _categoryRepositoryMock.Setup(r => r.GetAllAsync()).ReturnsAsync(categories);

            // Act
            var result = await _handler.Handle(query, CancellationToken.None);

            // Assert
            Assert.That(result, Is.Not.Null);
            Assert.That(result.Count(), Is.EqualTo(2));
        }
    }
}