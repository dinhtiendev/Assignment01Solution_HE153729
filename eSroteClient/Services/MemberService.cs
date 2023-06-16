using System;
using BussinessObject;
using eSroteClient.Services.IServices;

namespace eSroteClient.Services
{
	public class MemberService : BaseService, IMemberService
	{
        private readonly IHttpClientFactory _clientFactory;

        public MemberService(IHttpClientFactory clientFactory) : base(clientFactory)
        {
            _clientFactory = clientFactory;
        }

        public async Task<T> CreateMemberAsync<T>(MemberDto memberDto, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.POST,
                Data = memberDto,
                Url = SD.eStoreAPIBase + "/api/Members",
                AccessToken = token
            });
        }

        public async Task<T> DeleteMemberAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.DELETE,
                Url = SD.eStoreAPIBase + "/api/Members/" + id,
                AccessToken = token
            });
        }

        public async Task<T> GetAllMembersAsync<T>(string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.eStoreAPIBase + "/api/Members",
                AccessToken = token
            });
        }

        public async Task<T> GetMemberByIdAsync<T>(int id, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.GET,
                Url = SD.eStoreAPIBase + "/api/Members/" + id,
                AccessToken = token
            });
        }

        public async Task<T> UpdateMemberAsync<T>(MemberDto memberDto, string token)
        {
            return await this.SendAsync<T>(new ApiRequest()
            {
                ApiType = SD.ApiType.PUT,
                Data = memberDto,
                Url = SD.eStoreAPIBase + "/api/Members",
                AccessToken = token
            });
        }
    }
}

