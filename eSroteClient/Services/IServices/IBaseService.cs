using System;
using BussinessObject;

namespace eSroteClient.Services.IServices
{
	public interface IBaseService
	{
        ResponseDto responseModel { get; set; }
        Task<T> SendAsync<T>(ApiRequest apiRequest);
    }
}

