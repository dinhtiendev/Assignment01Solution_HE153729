using System;
using BussinessObject;

namespace DataAccess.Repositories.IRepositories
{
	public interface IMemberRepository
	{
		Task<IEnumerable<MemberDto>> GetMembers();
		Task<MemberDto> GetMemberById(int memberId);
		Task<MemberDto> CreateUpdateMember(MemberDto memberDto);
		Task<bool> DeleteMember(int memberId);
	}
}

