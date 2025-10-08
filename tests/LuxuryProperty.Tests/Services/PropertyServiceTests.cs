using Moq;
using LuxuryProperty.Application.Services;
using LuxuryProperty.Domain.Repositories;
using LuxuryProperty.Domain.Entities;
using LuxuryProperty.Domain.Common;

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
        MaxPrice = null,
        Page = 1,
        PageSize = 10
      };

      var properties = new List<PropertyWithImages>
            {
                new()
                {
                    IdProperty = "1",
                    Name = "Luxury Villa",
                    Address = "Miami",
                    Price = 1_000_000,
                    Images = new List<PropertyImage>
                    {
                        new() { IdPropertyImage = "img1", File = "villa1.jpg", Enabled = true }
                    }
                },
                new()
                {
                    IdProperty = "2",
                    Name = "Modern Apartment",
                    Address = "New York",
                    Price = 800_000,
                    Images = new List<PropertyImage>()
                }
            };

      var filtered = properties
              .Where(p => p.Name.Contains("Luxury"))
              .ToList();

      var pagedResult = new PagedResult<PropertyWithImages>
      {
        Items = filtered,
        TotalResults = filtered.Count,
        TotalPages = 1,
        CurrentPage = 1
      };

      _repositoryMock
                .Setup(r => r.GetByFiltersAsync(It.IsAny<PropertyFilters>()))
                .ReturnsAsync(pagedResult);

      // Act
      var result = await _service.GetPropertiesByFiltersAsync(filters);


      // Assert
      Assert.That(result.Items, Is.Not.Empty);
      Assert.Multiple(() =>
      {
        Assert.That(result.Items.Count(), Is.EqualTo(1));
        Assert.That(result.Items.First().Name, Is.EqualTo("Luxury Villa"));
        Assert.That(result.Items.First().Images, Is.Not.Empty);
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

    [Test]
    public async Task GetPropertyByIdAsync_ShouldCallRepository()
    {
      // Arrange
      string id = "123";

      _repositoryMock
          .Setup(r => r.GetByIdAsync(id))
          .ReturnsAsync(new PropertyWithImages());

      // Act
      await _service.GetPropertyByIdAsync(id);

      // Assert
      _repositoryMock.Verify(r => r.GetByIdAsync(id), Times.Once);
    }
  }
}
