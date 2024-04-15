using CoundAndEat.Api.Data;
using CoundAndEat.Api.Entities;
using CoundAndEat.Api.Interfaces;
using CoundAndEat.Dtos.Auth;
using Microsoft.AspNetCore.Identity;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;

namespace CoundAndEat.Api.Service
{
    public class AuthorizationService : IAuthorizationService
    {
        private readonly DataContext _context;
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole<long>> _roleManager;
        private readonly ITokenService _tokenService;
        public AuthorizationService(DataContext context,
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole<long>> roleManager,
            ITokenService tokenService)
        {
            _context = context;
            _userManager = userManager;
            _roleManager = roleManager;
            _tokenService = tokenService;
        }
      
        public async Task<LoginResponse> Login(LoginModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user != null && await _userManager.CheckPasswordAsync(user, model.Password))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));

                    var token = _tokenService.GetToken(authClaims);
                    var refreshToken = _tokenService.GetRefreshToken();
                    var tokenInfo = _context.TokenInfo.FirstOrDefault(a => a.Email == user.Email);
                    if (tokenInfo == null)
                    {
                        var info = new TokenInfo
                        {
                            Email = user.Email,
                            RefreshToken = refreshToken,
                            RefreshTokenExpiry = DateTime.UtcNow.AddDays(1)
                        };
                        _context.TokenInfo.Add(info);
                    }

                    else
                    {
                        tokenInfo.RefreshToken = refreshToken;
                        tokenInfo.RefreshTokenExpiry = DateTime.UtcNow.AddDays(1);
                    }
                    try
                    {
                        _context.SaveChanges();
                    }
                    catch (Exception ex)
                    {
                        return null;
                    }
                    return (new LoginResponse
                    {
                        Email = user.Email,
                        Id = user.Id,
                        Token = token.TokenString,
                        RefreshToken = refreshToken,
                        Expiration = token.ValidTo,
                        StatusCode = 200,
                        Message = "Вход успешен"
                    });

                }
            }
            //login failed condition

            return (
                new LoginResponse
                {
                    StatusCode = 0,
                    Message = "Invalid Username or Password",
                    Token = "",
                    Expiration = null
                });
        }

        public async Task<LoginResponse> Login(string email, string pass)
        {
            var user = await _userManager.FindByEmailAsync(email);
            if (user != null && await _userManager.CheckPasswordAsync(user, pass))
            {
                var userRoles = await _userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name, user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };
                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }

                var token = _tokenService.GetToken(authClaims);
                var refreshToken = _tokenService.GetRefreshToken();
                var tokenInfo = _context.TokenInfo.FirstOrDefault(a => a.Email == user.Email);
                if (tokenInfo == null)
                {
                    var info = new TokenInfo
                    {
                        Email = user.Email,
                        RefreshToken = refreshToken,
                        RefreshTokenExpiry = DateTime.UtcNow.AddDays(1)
                    };
                    _context.TokenInfo.Add(info);
                }

                else
                {
                    tokenInfo.RefreshToken = refreshToken;
                    tokenInfo.RefreshTokenExpiry = DateTime.UtcNow.AddDays(1);
                }
                try
                {
                    _context.SaveChanges();
                }
                catch (Exception ex)
                {
                    return null;
                }
                return (new LoginResponse
                {
                    Email = user.Email,
                    Id = user.Id,
                    Token = token.TokenString,
                    RefreshToken = refreshToken,
                    Expiration = token.ValidTo,
                    StatusCode = 200,
                    Message = "Вход успешен"
                });

            }
            //login failed condition

            return (
                new LoginResponse
                {
                    StatusCode = 0,
                    Message = "Invalid Username or Password",
                    Token = "",
                    Expiration = null
                });
        }

        public async Task<Status> Registration(RegistrationModel model)
        {
            var status = new Status();
            if (model == null)
            {
                status.StatusCode = 0;
                status.Message = "Please pass all the required fields";
                return (status);
            }
            // check if user exists
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
            {
                status.StatusCode = 0;
                status.Message = "Пользователь с такой электронной почтой уже существует";
                return (status);
            }
            var user = new ApplicationUser
            {
                UserName = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                Email = model.Email,
            };
            // create a user here
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                status.StatusCode = 0;
                status.Message = "User creation failed";
                return (status);
            }

            // add roles here
            // for admin registration UserRoles.Admin instead of UserRoles.Roles
            if (!await _roleManager.RoleExistsAsync(UserRoles.User))
                await _roleManager.CreateAsync(new IdentityRole<long>(UserRoles.User));

            if (await _roleManager.RoleExistsAsync(UserRoles.User))
            {
                await _userManager.AddToRoleAsync(user, UserRoles.User);
            }
            // Добавляю токен для подтверждения электронной почты

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            status.StatusCode = 200;
            status.Message = token;

            return (status);
        }
    }
}
