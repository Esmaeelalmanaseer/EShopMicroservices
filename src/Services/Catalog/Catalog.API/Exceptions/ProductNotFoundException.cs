
namespace Catalog.API.Exceptions;

public class ProductNotFoundException : NotFoundException
{
    public ProductNotFoundException(string userName) : base("Prodcut",userName)
    {
        
    }
}
