using AutoMapper;
using FCManager.IdentityManager.Contexts;
using FCManager.Models.Account;
using FCManager.Models.Models;
using FCManager.Models.ViewModels.Account;
using FCManager.Models.Wrappers;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Logging;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using Settings;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Cryptography;
using System.Text;
using System.Text.RegularExpressions;

namespace IdentityManager.Services
{
    public class AccountService : IAccountService
    {
        private readonly IdentityContext _identitycontext;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IMapper _mapper;
        private readonly JWTSettings _jwtSettings;
        private readonly ILogger<string> _logger;

        public AccountService(
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            UserManager<ApplicationUser> userManager,
            IdentityContext identitycontext,
            IMapper mapper,
            IOptions<JWTSettings> jwtSettings,
            ILogger<string> logger)
        {
            _roleManager = roleManager;
            _signInManager = signInManager;
            _userManager = userManager;
            _identitycontext = identitycontext;
            _mapper = mapper;
            _jwtSettings = jwtSettings.Value;
            _logger = logger;
        }


        public async Task<Response<AuthenticationResponse>> LoginAsync(LoginViewModel request, string ipAddress)
        {
            try
            {
                var user = new ApplicationUser();
                var emailRegex = @"[a-z0-9!#$%&'*+/=?^_`{|}~-]+(?:\.[a-z0-9!#$%&'*+/=?^_`{|}~-]+)*@(?:[a-z0-9](?:[a-z0-9-]*[a-z0-9])?\.)+[a-z0-9](?:[a-z0-9-]*[a-z0-9])?";

                bool isEmail = Regex.IsMatch(request.Email, emailRegex, RegexOptions.IgnoreCase);

                if (isEmail)
                {
                    //user = await _userManager.FindByEmailAsync(request.Email);
                    user = await _identitycontext.Users.FirstOrDefaultAsync(x => x.Email == request.Email);
                }
                else
                {
                    user = await _userManager.FindByNameAsync(request.Email);
                }

                if (user == null)
                {
                    return new Response<AuthenticationResponse> { Data = null, Succeeded = false, Message = $"Invalid Credentials for '{request.Email}'.", ResponseCode = "-1", StatusCode = 401 };
                }

                var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, lockoutOnFailure: false);
                //Test for valid password
                if (!result.Succeeded)
                {
                    return new Response<AuthenticationResponse> { Data = null, Succeeded = false, Message = $"Invalid Credentials for '{request.Email}'.", ResponseCode = "-1", StatusCode = 401 };
                }

                //Test if the user is active
                if (!user.IsActive)
                {
                    return new Response<AuthenticationResponse> { Data = null, Succeeded = false, Message = $"Account for '{request.Email}' is inactive.", ResponseCode = "-1", StatusCode = 401 };
                }

                JwtSecurityToken jwtSecurityToken = await GenerateJWToken(user);

                var response = _mapper.Map<AuthenticationResponse>(user);
                response.JWToken = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

                response.RoleName = _userManager.GetRolesAsync(user).Result.FirstOrDefault();

                var refreshToken = GenerateRefreshToken(ipAddress);
                response.RefreshToken = refreshToken.Token;
                return ApplicationResponse.SuccessMessage<AuthenticationResponse>(response, $"Authenticated {user.UserName}");
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, $"An error has occured during login process");
                throw ex;
            }
        }
        private async Task<JwtSecurityToken> GenerateJWToken(ApplicationUser user)
        {
            try
            {

                var userClaims = await _userManager.GetClaimsAsync(user);
                var roles = await _userManager.GetRolesAsync(user);
                var userrole = await (from u in _identitycontext.UserRoles
                                      join r in _identitycontext.Roles on u.RoleId equals r.Id
                                      where u.UserId == user.Id
                                      select new
                                      {
                                          u.UserId,
                                          u.RoleId,
                                          r.Name
                                      }).FirstOrDefaultAsync();//User is assumed to have one role for each account

                var roleClaims = new List<Claim>();

                for (int i = 0; i < roles.Count; i++)
                {
                    roleClaims.Add(new Claim("roles", roles[i]));
                }

                string ipAddress = Helpers.GetIpAddress();

                var claims = new[]
                {
                new Claim(JwtRegisteredClaimNames.Sub, user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                new Claim(JwtRegisteredClaimNames.Email, user.Email),
                new Claim("uid", user.Id),
                new Claim("email", user.Email),
                new Claim("roleid", userrole.RoleId),
                new Claim("rolename", userrole.Name)
            }
                .Union(userClaims)
                .Union(roleClaims);

                var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwtSettings.Key));
                var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

                var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwtSettings.Issuer,
                    audience: _jwtSettings.Audience,
                    claims: claims,
                    expires: DateTime.UtcNow.AddMinutes(_jwtSettings.DurationInMinutes),
                    signingCredentials: signingCredentials);
                return jwtSecurityToken;
            }
            catch (Exception ex)
            {
                _logger.LogError(ex.Message, "An error has occured when generating JWT Token");
                throw ex;
            }
        }

        private RefreshToken GenerateRefreshToken(string ipAddress)
        {
            return new RefreshToken
            {
                Token = RandomTokenString(),
                Expires = DateTime.UtcNow.AddDays(7),
                Created = DateTime.UtcNow,
                CreatedByIp = ipAddress
            };
        }

        private string RandomTokenString()
        {
            using var rngCryptoServiceProvider = new RNGCryptoServiceProvider();
            var randomBytes = new byte[40];
            rngCryptoServiceProvider.GetBytes(randomBytes);
            // convert random bytes to hex string
            return BitConverter.ToString(randomBytes).Replace("-", "");
        }
    }

}
