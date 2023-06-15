using System;
using BussinessObject;

namespace eSroteClient.Services.IServices
{
	public interface IMemberService
	{
        Task<T> GetAllMembersAsync<T>();
        Task<T> GetMemberByIdAsync<T>(int id);
        Task<T> CreateMemberAsync<T>(MemberDto memberDto);
        Task<T> UpdateMemberAsync<T>(MemberDto memberDto);
        Task<T> DeleteMemberAsync<T>(int id);
    }
}

