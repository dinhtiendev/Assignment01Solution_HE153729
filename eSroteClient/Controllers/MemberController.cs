using System;
using BussinessObject;
using eSroteClient.Services.IServices;
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
            List<MemberDto> list = new();
            var response = await _memberService.GetAllMembersAsync<ResponseDto>();
            if (response != null && response.IsSuccess)
            {
                list = JsonConvert.DeserializeObject<List<MemberDto>>(Convert.ToString(response.Result));
            }
            return View(list);
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
            if (ModelState.IsValid)
            {
                var response = await _memberService.CreateMemberAsync<ResponseDto>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(MemberIndex));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> MemberEdit(int id)
        {
            var response = await _memberService.GetMemberByIdAsync<ResponseDto>(id);
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
            if (ModelState.IsValid)
            {
                var response = await _memberService.UpdateMemberAsync<ResponseDto>(model);
                if (response != null && response.IsSuccess)
                {
                    return RedirectToAction(nameof(MemberIndex));
                }
            }
            return View(model);
        }

        public async Task<IActionResult> MemberDelete(int id)
        {
            var response = await _memberService.GetMemberByIdAsync<ResponseDto>(id);
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
            if (ModelState.IsValid)
            {
                var response = await _memberService.DeleteMemberAsync<ResponseDto>(model.MemberId);
                if (response.IsSuccess)
                {
                    return RedirectToAction(nameof(MemberIndex));
                }
            }
            return View(model);
        }
    }
}

