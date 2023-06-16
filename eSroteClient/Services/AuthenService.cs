using System;
using BussinessObject;
using eSroteClient.Services.IServices;

namespace eSroteClient.Services
{
	public class AuthenService : BaseService, IAuthenService
	{
        private readonly IHttpClientFactory _clientFactory;

        public AuthenService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }
            
        public async Task<T> Login<T>(AccountDto accountDto)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.eStoreAPIBase
                + "/api/Authentications?Email=" + accountDto.Email
                + "&Password=" + accountDto.Password,
                AccessToken = ""
            });
        }
    }
}

