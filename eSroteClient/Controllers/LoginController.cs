using System;
using System.IdentityModel.Tokens.Jwt;
using System.Net.Http.Headers;
using System.Security.Claims;
using System.Text;
using BussinessObject;
using eSroteClient.Services;
using eSroteClient.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;

namespace eSroteClient.Controllers
{
	public class LoginController : Controller
	{
        private readonly IAuthenService _authenService;

        public LoginController(IAuthenService authenService)
        {
            _authenService = authenService;
        }

        public async Task<IActionResult> LoginIndex()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> LoginIndex(AccountDto accountDto)
        {
            if (ModelState.IsValid)
            {
                var response = await _authenService.Login<ResponseDto>(accountDto);
                if (response != null && response.IsSuccess)
                {
                    var token = Convert.ToString(response.Result);

                    HttpContext.Session.SetString("token", token);
                    return RedirectToAction("Index", "Home");
                }
            }
            return View(accountDto);
        }

        public async Task<IActionResult> LogoutIndex()
        {
            HttpContext.Session.Clear();
            return RedirectToAction("Index", "Home");
        }
    }
}

