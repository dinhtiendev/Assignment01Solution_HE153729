using System;
using BussinessObject;
using DataAccess.Models;
using Microsoft.EntityFrameworkCore;

namespace DataAccess.DAOs
{
	public class MemberDAO
	{
		private Assignment1APIContext _dbContext;

		public MemberDAO()
		{
			_dbContext = DbContextSingleton.Instance;
		}

		public async Task<IEnumerable<Member>> GetMembersAsync()
		{
			return await _dbContext.Members.ToListAsync();
		}

        public async Task<Member> GetMemberByIdAsync(int memberId)
        {
            return await _dbContext.Members.Where(x => x.MemberId == memberId).FirstOrDefaultAsync();
        }

        public async Task CreateAsync(Member member)
        {
            _dbContext.Members.Add(member);
            await _dbContext.SaveChangesAsync();
        }

        public async Task UpdateAsync(Member oldMember,Member member)
        {
            _dbContext.Entry(oldMember).State = EntityState.Detached;
            _dbContext.Members.Update(member);
            await _dbContext.SaveChangesAsync();
        }

        public async Task DeleteAsync(Member member)
        {
            _dbContext.Members.Remove(member);
            await _dbContext.SaveChangesAsync();
        }

        public async Task<Member> GetMemberByEmailAndPasswordAsync(string email, string password)
        {
            return await _dbContext.Members.FirstOrDefaultAsync(x => x.Email == email && x.Password == password);
        }
    }
}

