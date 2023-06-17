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
        [HttpGet]
        public async Task<object> Authenticate(AccountDto accountDto)
        {
            try
            {
                MemberDto memberDto = await _memberRepository.GetMemberByEmailAndPassword(accountDto.Email, accountDto.Password);
                if (memberDto != null)
                {
                    var secretKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes("superSecretKey@345superSecretKey@345"));
                    var signinCredentials = new SigningCredentials(secretKey, SecurityAlgorithms.HmacSha256);
                    string role = "User";
                    if (accountDto.Email == _configuration["Admin:Email"] && accountDto.Password == _configuration["Admin:Password"])
                    {
                        role = "Admin";
                    }
                    var tokenOptions = new JwtSecurityToken(
                        issuer: "https://localhost:7049",
                        audience: "https://localhost:7042",
                        claims: new Claim[]
                        {
                            new Claim("MemberId", memberDto.MemberId.ToString()),
                            new Claim("Email", accountDto.Email),
                            new Claim("Role", role),
                            new Claim(ClaimTypes.Role, role)
                        },
                        expires: DateTime.Now.AddMinutes(60),
                        signingCredentials: signinCredentials
                    );
                    var tokenString = new JwtSecurityTokenHandler().WriteToken(tokenOptions);
                    _response.IsSuccess = true;
                    _response.Result = tokenString;
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

