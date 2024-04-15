﻿using CoundAndEat.Api.Data;
using CoundAndEat.Api.Interfaces;
using CoundAndEat.Dtos.Auth;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace CoundAndEat.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TokenController : ControllerBase
    {
        private readonly DataContext _ctx;
        private readonly ITokenService _service;
        public TokenController(DataContext ctx, ITokenService service)
        {
            _ctx = ctx;
            _service = service;

        }

        [HttpPost]
        [Route("refresh")]
        public IActionResult Refresh(RefreshTokenRequest tokenApiModel)
        {
            if (tokenApiModel is null)
                return BadRequest("Invalid client request");
            string accessToken = tokenApiModel.AccessToken;
            string refreshToken = tokenApiModel.RefreshToken;
            var principal = _service.GetPrincipalFromExpiredToken(accessToken);
            var username = principal.Identity.Name;
            var user = _ctx.TokenInfo.SingleOrDefault(u => u.Email == username);
            if (user is null || user.RefreshToken != refreshToken || user.RefreshTokenExpiry <= DateTime.UtcNow)
                return BadRequest("Invalid client request");
            var newAccessToken = _service.GetToken(principal.Claims.ToList());
            var newRefreshToken = _service.GetRefreshToken();
            user.RefreshToken = newRefreshToken;
            _ctx.SaveChanges();
            return Ok(new RefreshTokenRequest()
            {
                AccessToken = newAccessToken.TokenString,
                RefreshToken = newRefreshToken
            });
        }

        //revoken is use for removing token enntry
        [HttpPost, Authorize]
        [Route("revoke")]
        public IActionResult Revoke()
        {
            try
            {
                var username = User.Identity.Name;
                var user = _ctx.TokenInfo.SingleOrDefault(u => u.Email == username);
                if (user is null)
                    return BadRequest();
                user.RefreshToken = null;
                _ctx.SaveChanges();
                return Ok(true);
            }
            catch (Exception ex)
            {
                return BadRequest();
            }
        }
    }
}
