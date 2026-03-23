using Marten.Schema;

namespace Catalog.API.Data;

public class CatalogInitialData : IInitialData
{
    public async Task Populate(IDocumentStore store, CancellationToken cancellation)
    {
        using var seesion = await store.LightweightSerializableSessionAsync();
        if (await seesion.Query<Product>().AnyAsync())
            return;

        seesion.Store<Product>(GetPreconfiguredProducts());
        await seesion.SaveChangesAsync(cancellation);
    }
    private static IEnumerable<Product> GetPreconfiguredProducts()
    {
        return new List<Product>
        {
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "IPhone 13 Pro",
                Category = new List<string>{"Smart Phone"},
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec suscipit auctor dui, sed efficitur ipsum.",
                ImageFile = "product-1.png",
                Price = 999.00M,
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Samsung Galaxy S21",
                Category = new List<string>{"Smart Phone"},
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec suscipit auctor dui, sed efficitur ipsum.",
                ImageFile = "product-2.png",
                Price = 899.00M
            },
            new Product
            {
                Id = Guid.NewGuid(),
                Name = "Google Pixel 6",
                Category = new List<string>{"Smart Phone"},
                Description = "Lorem ipsum dolor sit amet, consectetur adipiscing elit. Donec suscipit auctor dui, sed efficitur ipsum.",
                ImageFile = "product-3.png",
                Price = 799.00M
            },
        };
    }
}
