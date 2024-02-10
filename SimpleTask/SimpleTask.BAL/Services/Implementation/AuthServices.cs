using Microsoft.AspNetCore.Identity;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using SimpleTask.BAL.DTOs;
using SimpleTask.BAL.Helpers;
using SimpleTask.BAL.Services.Interfaces;
using SimpleTask.DAL.Domains;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace SimpleTask.BAL.Services.Implementation
{
    public class AuthServices : IAuthServices
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly JWT _jwt;

        public AuthServices(
            UserManager<ApplicationUser> userManager,
            IOptions<JWT> jwt)
        {
            _userManager = userManager;
            _jwt = jwt.Value;
        }

        public async Task<string> GenerateToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
                roleClaims.Add(new Claim("roles", role));

            var claims = new[]
            {
                new Claim("Id", user.Id),
                new Claim("Email", user.Email),
                new Claim("Name", user.Name),
            }
            .Union(userClaims)
            .Union(roleClaims);

            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.UtcNow.ToLocalTime().AddHours(_jwt.DurationInHours),
                signingCredentials: signingCredentials);

            var Token = new JwtSecurityTokenHandler().WriteToken(jwtSecurityToken);

            return Token;
        }

        public async Task<AuthModel> LoginAsync(LogInDTo model)
        {
            var authModel = new AuthModel();

            var user = await _userManager.FindByEmailAsync(model.Email);

            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Email or Password is incorrect!";
                return authModel;
            }

            return await GetAuthModel(user);
        }

        private async Task<AuthModel> GetAuthModel(ApplicationUser user)
        {
            var roles = await _userManager.GetRolesAsync(user);
            return new AuthModel()
            {
                Token = await GenerateToken(user),
                Email = user.Email,
                Roles = roles.ToList(),
                IsAuthenticated = true
            };
        }

        public async Task<AuthModel> RegisterAsync(RegisterDto model)
        {
            if (await _userManager.FindByEmailAsync(model.Email) is not null)
                return new AuthModel { Message = "Email is already registered!" };

            var user = new ApplicationUser
            {
                Name = model.FullName,
                Email = model.Email,
                UserName = model.Email
            };
            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Empty;

                foreach (var error in result.Errors)
                    errors += $"{error.Description},";

                return new AuthModel { Message = errors };
            }

            await _userManager.AddToRoleAsync(user, "User");

            return new AuthModel
            {
                Email = user.Email,
                Message = "User Succefuly Registred",
                IsAuthenticated = true,
            };
        }
    }
}