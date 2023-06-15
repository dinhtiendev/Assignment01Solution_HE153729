using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using AutoMapper;
using BussinessObject;
using DataAccess.DAOs;
using DataAccess.Models;
using DataAccess.Repositories.IRepositories;
using Microsoft.IdentityModel.Tokens;

namespace DataAccess.Repositories
{
	public class MemberRepository : IMemberRepository
	{
        private MemberDAO _memberDAO;
        private IMapper _mapper;

		public MemberRepository(MemberDAO memberDAO, IMapper mapper)
		{
            _memberDAO = memberDAO;
            _mapper = mapper;
        }

        public async Task<IEnumerable<MemberDto>> GetMembers()
        {
            IEnumerable<Member> memberList = await _memberDAO.GetMembersAsync();
            return _mapper.Map<List<MemberDto>>(memberList);
        }       

        public async Task<MemberDto> GetMemberById(int memberId)
        {
            Member member = await _memberDAO.GetMemberByIdAsync(memberId);
            return _mapper.Map<MemberDto>(member);
        }

        public async Task<MemberDto> CreateUpdateMember(MemberDto memberDto)
        {
            Member member = _mapper.Map<MemberDto, Member>(memberDto);
            Member oldMember = await _memberDAO.GetMemberByIdAsync(member.MemberId);
            if (oldMember != null)
            {
                await _memberDAO.UpdateAsync(oldMember, member);
            } else
            {
                await _memberDAO.CreateAsync(member);
            }
            return _mapper.Map<Member, MemberDto>(member);
        }

        public async Task<bool> DeleteMember(int memberId)
        {
            try
            {
                Member member = await _memberDAO.GetMemberByIdAsync(memberId);
                if (member == null)
                {
                    return false;
                }
                await _memberDAO.DeleteAsync(member);
                return true;
            }
            catch (Exception ex)
            {
                return false;
            }
        }

        public async Task<MemberDto> GetMemberByEmailAndPassword(string email, string password)
        {
            var member = await _memberDAO.GetMemberByEmailAndPasswordAsync(email, password);
            if (member != null)
            {
                return _mapper.Map<MemberDto>(member);
            }
            return null;
        }
    }
}

