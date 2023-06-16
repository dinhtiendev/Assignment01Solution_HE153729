using System;
using BussinessObject;

namespace eSroteClient.Services.IServices
{
	public interface IAuthenService
	{
        Task<T> Login<T>(AccountDto accountDto);
    }
}

