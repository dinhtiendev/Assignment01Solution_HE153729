using System;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using System.Text;
using BussinessObject;
using DataAccess.Repositories.IRepositories;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.IdentityModel.Tokens;

namespace eStoreAPI.Controllers
{
    [Route("api/Authentications")]
    public class AuthenticationAPIController : Controller
	{
        protected ResponseDto _response;
        private IMemberRepository _memberRepository;
        private readonly IConfiguration _configuration;

        public AuthenticationAPIController(IMemberRepository memberRepository, IConfiguration configuration)
        {
            _memberRepository = memberRepository;
            _configuration = configuration;
            _response = new ResponseDto();
        }

        [AllowAnonymous]
        [HttpPost]
        public async Task<object> Authenticate(string email, string password)
        {
            try
            {
                MemberDto memberDto = await _memberRepository.GetMemberByEmailAndPassword(email, password);
                if (memberDto != null)
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345superSecretKey@345"));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    string role = "User";
                    if (email == _configuration["Admin:Email"] && password == _configuration["Admin:Password"])
                    {
                        role = "Admin";
                    }
                    var tokenOptions = new JwtSecurityToken(
                        issuer: "https://localhost:7042",
                        audience: "https://localhost:7042",
                        claims: new Claim[]
                        {
                            new Claim(ClaimTypes.Name, email),
                            new Claim(ClaimTypes.Role, role)
                        },
                        expires: DateTime.Now.AddMinutes(60),
                        signingCredentials: signinCredentials
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    return tokenString;
                }

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

