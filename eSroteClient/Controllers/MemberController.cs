using System;
using System.Diagnostics;
using BussinessObject;
using eSroteClient.Models;
using eSroteClient.Services.IServices;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Newtonsoft.Json;

namespace eSroteClient.Controllers
{
	public class MemberController : Controller
	{
        private readonly IMemberService _memberService;

        public MemberController(IMemberService memberService)
        {
            _memberService = memberService;
        }

        public async Task<IActionResult> MemberIndex()
        {
            var token = HttpContext.Session.GetString("token");
            if (token != null)
            {
                List<MemberDto> list = new();
                var response = await _memberService.GetAllMembersAsync<ResponseDto>(token);
                if (response != null && response.IsSuccess)
                {
                    list = JsonConvert.DeserializeObject<List<MemberDto>>(Convert.ToString(response.Result));
                }
                return View(list);
            }
            return NotFound();

        }

        //[HttpPost]
        //public async Task<IActionResult> MemberIndex(DateTime MemberDate)
        //{
        //    if (ModelState.IsValid)
        //    {
        //        List<MemberDto> list = new();
        //        var response = await _memberService.GetAllMembersByDateAsync<ResponseDto>(MemberDate);
        //        if (response != null && response.IsSuccess)
        //        {
        //            list = JsonConvert.DeserializeObject<List<MemberDto>>(Convert.ToString(response.Result));
        //        }
        //        ViewData["MemberDate"] = MemberDate.ToString("yyyy-MM-dd");
        //        return View(list);
        //    }
        //    return View(MemberDate);
        //}

        public async Task<IActionResult> MemberCreate()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> MemberCreate(MemberDto model)
        {
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var response = await _memberService.CreateMemberAsync<ResponseDto>(model, token);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(MemberIndex));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> MemberEdit(int memberId)
        {
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return NotFound();
            }
            var response = await _memberService.GetMemberByIdAsync<ResponseDto>(memberId, token);
            if (response != null && response.IsSuccess)
            {
                MemberDto model = JsonConvert.DeserializeObject<MemberDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> MemberEdit(MemberDto model)
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
                    return RedirectToAction(nameof(MemberIndex));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> MemberDelete(int memberId)
        {
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return NotFound();
            }
            var response = await _memberService.GetMemberByIdAsync<ResponseDto>(memberId, token);
            if (response != null && response.IsSuccess)
            {
                MemberDto model = JsonConvert.DeserializeObject<MemberDto>(Convert.ToString(response.Result));
                return View(model);
            }
            return NotFound();
        }
        [HttpPost]
        public async Task<IActionResult> MemberDelete(MemberDto model)
        {
            var token = HttpContext.Session.GetString("token");
            if (token == null)
            {
                return NotFound();
            }
            if (ModelState.IsValid)
            {
                var response = await _memberService.DeleteMemberAsync<ResponseDto>(model.MemberId, token);
                if (response == null)
                {
                    ModelState.AddModelError("", "Bạn không có quyền để xoá.");
                    return View(model);
                }
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(MemberIndex));
                }
            }
            return View(model);
        }
    }
}

