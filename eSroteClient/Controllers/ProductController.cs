using System;
using System.Reflection;
using BussinessObject;
using eSroteClient.Services;
using eSroteClient.Services.IServices;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace eSroteClient.Controllers
{
	public class ProductController : Controller
    {
        private readonly IProductService _productService;
        private readonly ICategoryService _categoryService;

        public ProductController(IProductService productService, ICategoryService categoryService)
        {
            _productService = productService;
            _categoryService = categoryService;
        }

        public async Task<IActionResult> ProductIndex()
        {
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return NotFound();
            }
            List<ProductDto> list = new();
            var response = await _productService.GetAllProductsAsync<ResponseDto>(token);
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<ProductDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        public async Task<IActionResult> ProductCreate()
        {
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return NotFound();
            }
            List<CategoryDto> listCategory = new();
            var responseProduct = await _categoryService.GetAllCategoriesAsync<ResponseDto>(token);
            if (responseProduct != null && responseProduct.IsSuccess)
            {
                listCategory = JsonConvert.DeserializeObject<List<CategoryDto>>(Convert.ToString(responseProduct.Result));
            }
            ViewData["listCategory"] = listCategory;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> ProductCreate(ProductDto model)
        {
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var response = await _productService.CreateProductAsync<ResponseDto>(model, token);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ProductEdit(int productId)
        {
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return NotFound();
            }
            List<CategoryDto> listCategory = new();
            var responseProduct = await _categoryService.GetAllCategoriesAsync<ResponseDto>(token);
            if (responseProduct != null && responseProduct.IsSuccess)
            {
                listCategory = JsonConvert.DeserializeObject<List<CategoryDto>>(Convert.ToString(responseProduct.Result));
            }
            ViewData["listCategory"] = listCategory;
            var response = await _productService.GetProductByIdAsync<ResponseDto>(productId, token);
            if (response != null && response.IsSuccess)
            {
                ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> ProductEdit(ProductDto model)
        {
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var response = await _productService.UpdateProductAsync<ResponseDto>(model, token);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> ProductDelete(int productId)
        {
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return NotFound();
            }
            var response = await _productService.GetProductByIdAsync<ResponseDto>(productId, token);
            if (response != null && response.IsSuccess)
            {
                ProductDto model = JsonConvert.DeserializeObject<ProductDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> ProductDelete(ProductDto model)
        {
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var response = await _productService.DeleteProductAsync<ResponseDto>(model.ProductId, token);
                if (response == null)
                {
                    ModelState.AddModelError("", "Bạn không có quyền để xoá.");
                    return View(model);
                }
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(ProductIndex));
                }
            }
            return View(model);
        }
    }
}

