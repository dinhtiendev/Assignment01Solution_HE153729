using System;
using BussinessObject;

namespace DataAccess.Repositories.IRepositories
{
	public interface IProductRepository
	{
        Task<IEnumerable<ProductDto>> GetProducts();
        Task<ProductDto> GetProductById(int ProductId);
        Task<ProductDto> CreateUpdateProduct(ProductDto productDto);
        Task<bool> DeleteProduct(int productId);
    }
}

