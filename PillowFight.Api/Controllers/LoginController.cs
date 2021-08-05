﻿using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.DependencyInjection;
using PillowFight.Api.Models;
using PillowFight.BusinessServices;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;

namespace PillowFight.Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LoginController : ControllerBase
    {
        readonly IPlayerBL _playerBL;

        public LoginController(IServiceProvider serviceProvider)
        {
            _playerBL = serviceProvider.GetRequiredService<IPlayerBL>();
        }

        [HttpPost("Register")]
        public async Task<ActionResult> Register(PlayerRegistrationDetails details)
        {
            _playerBL.CreatePlayer(details.UserName, details.Password, details.Email);
            return Accepted();
        }

        [HttpPost("Login")]
        public async Task<ActionResult<PlayerDetails>> LogIn(PlayerLoginDetails details)
        {
            Repositories.Models.Player player;

            player = _playerBL.GetPlayer(details.UserName, details.Password);
            if (player == null)
            {
                return NotFound();
            }

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.Name, details.UserName),
            };

            var claimsIdentity = new ClaimsIdentity(
                claims, CookieAuthenticationDefaults.AuthenticationScheme);

            var authProperties = new AuthenticationProperties
            {
                IsPersistent = true
            };

            await HttpContext.SignInAsync(
                CookieAuthenticationDefaults.AuthenticationScheme,
                new ClaimsPrincipal(claimsIdentity),
                authProperties);

            return Ok(new PlayerDetails()
            {
                UserName = player.Name,
                Email = player.Email,
                Wins = player.Wins,
                Losses = player.Losses
            });
        }
    }
}
