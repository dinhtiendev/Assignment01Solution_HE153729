using System;
using BussinessObject;
using eSroteClient.Services;
using eSroteClient.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eSroteClient.Controllers
{
	public class OrderController : Controller
	{
        private readonly IOrderService _orderService;
        private readonly IMemberService _memberService;
        private readonly IProductService _productService;

        public OrderController(IOrderService orderService,
            IMemberService memberService,
            IProductService productService)
        {
            _orderService = orderService;
            _memberService = memberService;
            _productService = productService;
        }

        public async Task<IActionResult> OrderIndex()
        {
            List<OrderDto> list = new();
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return NotFound();
            }
            var response = await _orderService.GetAllOrdersAsync<ResponseDto>(token);
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<OrderDto>>(Convert.ToString(response.Result));
            }
            return View(list);
        }

        public async Task<IActionResult> OrderCreate()
        {
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return NotFound();
            }
            List<ProductOrderDto> listProduct = new();
            var responseProduct = await _productService.GetAllProductsAsync<ResponseDto>(token);
            if (responseProduct != null && responseProduct.IsSuccess)
            {
                listProduct = JsonConvert.DeserializeObject<List<ProductOrderDto>>(Convert.ToString(responseProduct.Result));
            }
            List<MemberDto> listMember = new();
            var responseEmployee = await _memberService.GetAllMembersAsync<ResponseDto>(token);
            if (responseEmployee != null && responseEmployee.IsSuccess)
            {
                listMember = JsonConvert.DeserializeObject<List<MemberDto>>(Convert.ToString(responseEmployee.Result));
            }
            ViewData["listProduct"] = listProduct;
            ViewData["listMember"] = listMember;
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> OrderCreate(OrderDto model)
        {
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid && !model.ProductList.GroupBy(x => x.ProductId).Any(g => g.Count() > 1))
            {
                var response = await _orderService.CreateOrderAsync<ResponseDto>(model, token);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(OrderIndex));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> OrderEdit(int orderId)
        {
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return NotFound();
            }
            List<ProductOrderDto> listProduct = new();
            var responseProduct = await _productService.GetAllProductsAsync<ResponseDto>(token);
            if (responseProduct != null && responseProduct.IsSuccess)
            {
                listProduct = JsonConvert.DeserializeObject<List<ProductOrderDto>>(Convert.ToString(responseProduct.Result));
            }
            List<MemberDto> listMember = new();
            var responseEmployee = await _memberService.GetAllMembersAsync<ResponseDto>(token);
            if (responseEmployee != null && responseEmployee.IsSuccess)
            {
                listMember = JsonConvert.DeserializeObject<List<MemberDto>>(Convert.ToString(responseEmployee.Result));
            }
            ViewData["listProduct"] = listProduct;
            ViewData["listMember"] = listMember;
            var response = await _orderService.GetOrderByIdAsync<ResponseDto>(orderId, token);
            if (response != null && response.IsSuccess)
            {
                OrderDto model = JsonConvert.DeserializeObject<OrderDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> OrderEdit(OrderDto model)
        {
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var response = await _orderService.UpdateOrderAsync<ResponseDto>(model, token);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(OrderIndex));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> OrderDelete(int orderId)
        {
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return NotFound();
            }
            var response = await _orderService.GetOrderByIdAsync<ResponseDto>(orderId, token);
            if (response != null && response.IsSuccess)
            {
                OrderDto model = JsonConvert.DeserializeObject<OrderDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> OrderDelete(OrderDto model)
        {
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var response = await _orderService.DeleteOrderAsync<ResponseDto>(model.OrderId, token);
                if (response == null)
                {
                    ModelState.AddModelError("", "Bạn không có quyền để xoá.");
                }
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(OrderIndex));
                }
            }
            return View(model);
        }
    }
}

