using System;
using BussinessObject;
using DataAccess.Models;
using DataAccess.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace eStoreAPI.Controllers
{
    [Route("api/Members")]
    [Authorize]
    public class MemberAPIController : ControllerBase
    {
        protected ResponseDto _response;
        private IMemberRepository _memberRepository;

        public MemberAPIController(IMemberRepository memberRepository)
        {
            _memberRepository = memberRepository;
            _response = new ResponseDto();
        }

        [HttpGet]
        public async Task<object> Get()
        {
            try
            {
                IEnumerable<MemberDto> memberDtos = await _memberRepository.GetMembers();
                _response.Result = memberDtos;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpGet]
        [Route("{id}")]
        public async Task<object> Get(int id)
        {
            try
            {
                MemberDto memberDto = await _memberRepository.GetMemberById(id);
                _response.Result = memberDto;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPost]
        public async Task<object> Post([FromBody] MemberDto memberDto)
        {
            try
            {
                MemberDto model = await _memberRepository.CreateUpdateMember(memberDto);
                _response.Result = model;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpPut]
        public async Task<object> Put([FromBody] MemberDto memberDto)
        {
            try
            {
                MemberDto model = await _memberRepository.CreateUpdateMember(memberDto);
                _response.Result = model;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }

        [HttpDelete]
        [Route("{id}")]
        [Authorize(Roles = "Admin")]
        public async Task<object> Delete(int id)
        {
            try
            {
                bool isSuccess = await _memberRepository.DeleteMember(id);
                _response.Result = isSuccess;
            }
            catch (Exception ex)
            {
                _response.IsSuccess = false;
                _response.ErrorMessages = new List<string>() { ex.ToString() };
            }
            return _response;
        }
    }
}

