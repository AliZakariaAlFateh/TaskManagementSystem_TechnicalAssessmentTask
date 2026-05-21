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
    public class RegisterCommandHandler : IRequestHandler<RegisterCommand, ApiResponse<AuthResponseDto>>
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IConfiguration _configuration;

        public RegisterCommandHandler(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _configuration = configuration;
        }

        public async Task<ApiResponse<AuthResponseDto>> Handle(RegisterCommand request, CancellationToken cancellationToken)
        {
            // Check if user already exists
            var existingUser = await _userManager.FindByEmailAsync(request.Email);
            if (existingUser != null)
                return ApiResponse<AuthResponseDto>.FailResponse("User with this email already exists");

            var user = new ApplicationUser
            {
                UserName = request.Username,
                Email = request.Email
            };

            var result = await _userManager.CreateAsync(user, request.Password);

            if (!result.Succeeded)
            {
                var errors = result.Errors.Select(e => e.Description).ToList();
                return ApiResponse<AuthResponseDto>.FailResponse("Registration failed", errors);
            }

            string roleToAssign = "User";

            if (_userManager.Users.Count() == 1)
                roleToAssign = "Admin";

            await _userManager.AddToRoleAsync(user, roleToAssign);

            var response = new AuthResponseDto
            {
                Token = await GenerateJwtToken(user),  
                Email = user.Email,
                Username = user.UserName,
                Roles = (await _userManager.GetRolesAsync(user)).ToList()
            };

            return ApiResponse<AuthResponseDto>.SuccessResponse(response, "User registered successfully");
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
