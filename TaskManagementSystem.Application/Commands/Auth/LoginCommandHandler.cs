using MediatR;
using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;
using TaskManagementSystem.Application.DTOs.Auth;
using TaskManagementSystem.Domain.Entities;
using TaskManagementSystem.Shared.Responses;

namespace TaskManagementSystem.Application.Commands.Auth
{
    public class LoginCommandHandler : IRequestHandler<LoginCommand, ApiResponse<AuthResponseDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IConfiguration _configuration;

        public LoginCommandHandler(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _configuration = configuration;
        }

        public async Task<ApiResponse<AuthResponseDto>> Handle(LoginCommand request, CancellationToken cancellationToken)
        {
            var user = await _userManager.FindByEmailAsync(request.Email);
            if (user == null)
                return ApiResponse<AuthResponseDto>.FailResponse("Invalid email or password");

            var result = await _signInManager.PasswordSignInAsync(user.UserName, request.Password, false, false);

            if (!result.Succeeded)
                return ApiResponse<AuthResponseDto>.FailResponse("Invalid email or password");

            var response = new AuthResponseDto
            {
                Token = await GenerateJwtToken(user),  
                Email = user.Email,
                Username = user.UserName,
                Roles = (await _userManager.GetRolesAsync(user)).ToList()
            };

            return ApiResponse<AuthResponseDto>.SuccessResponse(response, "Login successful");
        }

        private async Task<string> GenerateJwtToken(ApplicationUser user)  
        {
            var key = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["Jwt:Key"]));
            var creds = new SigningCredentials(key, SecurityAlgorithms.HmacSha256);

            var roles = await _userManager.GetRolesAsync(user);

            var claims = new List<Claim>
            {
                new Claim(ClaimTypes.NameIdentifier, user.Id),
                new Claim(ClaimTypes.Email, user.Email),
                new Claim(ClaimTypes.Name, user.UserName)
            };

            
            foreach (var role in roles)
            {
                claims.Add(new Claim(ClaimTypes.Role, role));
            }

            var token = new JwtSecurityToken(
                issuer: _configuration["Jwt:Issuer"],
                audience: _configuration["Jwt:Audience"],
                claims: claims,
                expires: DateTime.UtcNow.AddDays(7),
                signingCredentials: creds
            );

            return new JwtSecurityTokenHandler().WriteToken(token);
        }
    }
}
