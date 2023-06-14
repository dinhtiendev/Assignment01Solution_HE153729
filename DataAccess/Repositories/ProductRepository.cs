using System;
using AutoMapper;
using BussinessObject;
using DataAccess.DAOs;
using DataAccess.Models;
using DataAccess.Repositories.IRepositories;

namespace DataAccess.Repositories
{
	public class ProductRepository : IProductRepository
	{
        private ProductDAO _productDAO;
        private IMapper _mapper;

        public ProductRepository(ProductDAO productDAO, IMapper mapper)
        {
            _productDAO = productDAO;
            _mapper = mapper;
        }

        public async Task<IEnumerable<ProductDto>> GetProducts()
        {
            IEnumerable<Product> productList = await _productDAO.GetProductsAsync();
            return _mapper.Map<List<ProductDto>>(productList);
        }

        public async Task<ProductDto> GetProductById(int productId)
        {
            Product product = await _productDAO.GetProductByIdAsync(productId);
            return _mapper.Map<ProductDto>(product);
        }

        public async Task<ProductDto> CreateUpdateProduct(ProductDto productDto)
        {
            Product product = _mapper.Map<ProductDto, Product>(productDto);
            Product oldProduct = await _productDAO.GetProductByIdAsync(product.ProductId);
            if (oldProduct != null)
            {
                await _productDAO.UpdateAsync(oldProduct, product);
            }
            else
            {
                await _productDAO.CreateAsync(product);
            }
            return _mapper.Map<Product, ProductDto>(product);
        }

        public async Task<bool> DeleteProduct(int productId)
        {
            try
            {
                Product product = await _productDAO.GetProductByIdAsync(productId);
                if (product == null)
                {
                    return false;
                }
                await _productDAO.DeleteAsync(product);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }
    }
}

