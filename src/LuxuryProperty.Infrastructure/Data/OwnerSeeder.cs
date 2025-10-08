using LuxuryProperty.Domain.Entities;
using LuxuryProperty.Infrastructure.Database;

namespace LuxuryProperty.Infrastructure.Data
{
  public class OwnerSeeder(MongoDbContext dbContext)
  {
    private readonly MongoDbContext _dbContext = dbContext;

    public async Task SeedAsync()
    {
      var owners = new List<Owner>();

      for (int i = 1; i <= 25; i++)
      {
        owners.Add(new Owner
        {
          IdOwner = Guid.NewGuid().ToString(),
          Name = $"Owner {i}",
          Address = $"Street {i}, City X",
          Photo = $"https://azfd-prod.millionluxury.com/spinfile/wwwroot/dist-front/img/MainSite/1-Rivage-24.5M.jpg",
          Birthday = DateTime.Now.AddYears(-i)

        });
      }

      await _dbContext.SeedOwnersAsync(owners);
    }
  }
}
