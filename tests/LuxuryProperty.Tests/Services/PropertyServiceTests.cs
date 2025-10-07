using Moq;
using LuxuryProperty.Application.Services;
using LuxuryProperty.Domain.Repositories;
using LuxuryProperty.Domain.Entities;

namespace LuxuryProperty.Tests.Services
{
  [TestFixture]
  public class PropertyServiceTests
  {
    private Mock<IPropertyRepository> _repositoryMock = null!;
    private PropertyService _service = null!;

    [SetUp]
    public void Setup()
    {
      _repositoryMock = new Mock<IPropertyRepository>();
      _service = new PropertyService(_repositoryMock.Object);
    }

    [Test]
    public async Task GetAllAsync_ShouldReturnFilteredProperties()
    {
      // Arrange
      var filters = new PropertyFilters
      {
        Name = "Luxury",
        Address = null,
        MinPrice = null,
        MaxPrice = null
      };

      var properties = new List<Property>
            {
                new() { Id = "1", Name = "Luxury Villa", Address = "Miami", Price = 1_000_000 },
                new() { Id = "2", Name = "Modern Apartment", Address = "New York", Price = 800_000 }
            };

      _repositoryMock
          .Setup(r => r.GetByFiltersAsync(filters))
          .ReturnsAsync(properties.Where(p => p.Name.Contains("Luxury")));

      // Act
      var result = await _repositoryMock.Object.GetByFiltersAsync(filters);

      // Assert
      Assert.That(result, Is.Not.Empty);
      Assert.Multiple(() =>
      {
        Assert.That(result.Count(), Is.EqualTo(1));
        Assert.That(result.First().Name, Is.EqualTo("Luxury Villa"));
      });

    }

    [Test]
    public async Task CreateAsync_ShouldCallRepository()
    {
      // Arrange
      var property = new Property
      {
        Id = "1",
        Name = "Penthouse",
        Address = "Los Angeles",
        Price = 2_000_000
      };

      _repositoryMock
          .Setup(r => r.CreateAsync(It.IsAny<Property>()))
          .Returns(Task.CompletedTask);

      // Act
      await _service.CreatePropertyAsync(property);

      // Assert
      _repositoryMock.Verify(r => r.CreateAsync(property), Times.Once);
    }

    [Test]
    public async Task DeleteAsync_ShouldCallRepository()
    {
      // Arrange
      string id = "123";

      _repositoryMock
          .Setup(r => r.DeleteAsync(id))
          .Returns(Task.CompletedTask);

      // Act
      await _service.DeletePropertyAsync(id);

      // Assert
      _repositoryMock.Verify(r => r.DeleteAsync(id), Times.Once);
    }
  }
}
