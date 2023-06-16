using System;
using BussinessObject;

namespace eSroteClient.Services.IServices
{
	public interface IMemberService
	{
        Task<T> GetAllMembersAsync<T>(string token);
        Task<T> GetMemberByIdAsync<T>(int id, string token);
        Task<T> CreateMemberAsync<T>(MemberDto memberDto, string token);
        Task<T> UpdateMemberAsync<T>(MemberDto memberDto, string token);
        Task<T> DeleteMemberAsync<T>(int id, string token);
    }
}

