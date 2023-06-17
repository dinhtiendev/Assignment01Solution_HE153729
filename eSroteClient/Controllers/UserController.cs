using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using BussinessObject;
using eSroteClient.Services.IServices;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eSroteClient.Controllers
{
	public class UserController : Controller
	{
        private readonly IMemberService _memberService;

        public UserController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public async Task<IActionResult> Profile()
        {
            var token = HttpContext.Session.GetString("token");
            if (token != null)
            {
                var handler = new JwtSecurityTokenHandler();
                var jwtSecurityToken = handler.ReadJwtToken(token);
                string memberId = jwtSecurityToken.Claims.FirstOrDefault(c => c.Type == "MemberId")?.Value;
                MemberDto model = new();
                var response = await _memberService.GetMemberByIdAsync<ResponseDto>(Int32.Parse(memberId), token);
                if (response != null && response.IsSuccess)
                {
                    model = JsonConvert.DeserializeObject<MemberDto>(Convert.ToString(response.Result));
                }
                return View(model);
            }
            return NotFound();
        }

        public async Task<IActionResult> UserEdit(MemberDto model)
        {
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var response = await _memberService.UpdateMemberAsync<ResponseDto>(model, token);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(Profile));
                }
            }
            return View(model);
        }
    }
}

