using AutoMapper;
using Istijara.Core.DTOs;
using Istijara.Core.DTOs.Identity;
using Istijara.Core.Entities;
using Istijara.Core.Interfaces.Services;
using Istijara.Service.Configurations;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.WebUtilities;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.IdentityModel.Tokens;
using System.ComponentModel.DataAnnotations;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;

namespace Istijara.Service.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly IMapper _mapper;
        private readonly JWT _jwt;
        private readonly IEmailService _emailService;


        public AuthService(UserManager<ApplicationUser> userManager, IMapper mapper, IOptions<JWT> jwt, RoleManager<IdentityRole> roleManager, IEmailService emailService)
        {
            _userManager = userManager;
            _mapper = mapper;
            _jwt = jwt.Value;
            _roleManager = roleManager;
            _emailService = emailService;
        }

        public async Task<AuthModel> RegisterAsync(RegisterModel model)
        {

            var isEmail = new EmailAddressAttribute().IsValid(model.EmailOrPhone);

            var existingUser = isEmail
                ? await _userManager.FindByEmailAsync(model.EmailOrPhone)
                : await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == model.EmailOrPhone);


            if (existingUser is not null)
                return new AuthModel { Message = "User already registered" };

            var user = _mapper.Map<ApplicationUser>(model);

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new AuthModel { Message = errors };
            }

            await _userManager.AddToRoleAsync(user, "User");

            var token = await CreateJwtToken(user);

            return new AuthModel
            {
                Message = "Email was registered successfully",
                Email = user.Email,
                Username = isEmail ? model.EmailOrPhone.Split('@')[0] : model.EmailOrPhone,
                PhoneNumber = user.PhoneNumber,
                IsAuthenticated = true,
                ExpiresOn = token.ValidTo,
                Roles = new List<string> { "User" },
                Token = new JwtSecurityTokenHandler().WriteToken(token)
            };
        }





        public async Task<AuthModel> GetTokenAsync(RequestTokenModel model)
        {
            var authModel = new AuthModel();

            var user = model.EmailOrPhone.Contains("@")
                ? await _userManager.FindByEmailAsync(model.EmailOrPhone)
                : await _userManager.Users.FirstOrDefaultAsync(u => u.PhoneNumber == model.EmailOrPhone);

            if (user is null || !await _userManager.CheckPasswordAsync(user, model.Password))
            {
                authModel.Message = "Email/Phone or Password is incorrect!";
                return authModel;
            }

            var token = await CreateJwtToken(user);
            authModel.Message = "Token created successfully";
            authModel.IsAuthenticated = true;
            authModel.Email = user.Email;
            authModel.PhoneNumber = user.PhoneNumber;
            authModel.Username = user.UserName;
            authModel.Roles = (await _userManager.GetRolesAsync(user)).ToList();
            authModel.ExpiresOn = token.ValidTo;
            authModel.Token = new JwtSecurityTokenHandler().WriteToken(token);

            return authModel;
        }


        public async Task<string> AddRoleAsync(AddRoleModel model)
        {
            var user = await _userManager.FindByIdAsync(model.UserId);

            if (user is null || !await _roleManager.RoleExistsAsync(model.Role))
                return "Invalid User ID or Role";

            if (await _userManager.IsInRoleAsync(user, model.Role))
                return "User already assigned to this role";

            var result = await _userManager.AddToRoleAsync(user, model.Role);

            return result.Succeeded ? string.Empty : "Something went wrong";

        }


        public async Task<bool> ForgotPasswordAsync(ForgotPasswordModel model)
        {


            var user = await _userManager.FindByEmailAsync(model.Email!);

            if (user is null)
                return false;


            var token = await _userManager.GeneratePasswordResetTokenAsync(user);


            var param = new Dictionary<string, string?>
            {
                {"token",token},
                {"email",model.Email }
            };


            var callback = QueryHelpers.AddQueryString(model.ClientUri, param);

            var message = new EmailMessage([user.Email], "Reset Password Token", callback);
            await _emailService.SendEmailAsync(message);

            return true;

        }


        public async Task<ResetPasswordResult> ResetPasswordAsync(ResetPasswordModel model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email!);

            if (user is null)
                return new ResetPasswordResult(false, "user not found Request");


            var result = await _userManager.ResetPasswordAsync(user, model.Token!, model.Password!);

            if (!result.Succeeded)
            {
                var errors = string.Join(", ", result.Errors.Select(e => e.Description));
                return new ResetPasswordResult(false, errors);

            }

            return new ResetPasswordResult(true, "Password Changed Successfully");

        }










        private async Task<JwtSecurityToken> CreateJwtToken(ApplicationUser user)
        {
            var userClaims = await _userManager.GetClaimsAsync(user);
            var roles = await _userManager.GetRolesAsync(user);
            var roleClaims = new List<Claim>();

            foreach (var role in roles)
            {
                roleClaims.Add(new Claim("roles", role));
            }

            var claims = new List<Claim>
            {
                new Claim(JwtRegisteredClaimNames.Sub,user.UserName),
                new Claim(JwtRegisteredClaimNames.Jti,Guid.NewGuid().ToString()),
                new Claim("uid",user.Id)
            };

            if (!string.IsNullOrWhiteSpace(user.Email))
                claims.Add(new Claim(JwtRegisteredClaimNames.Email, user.Email));

            if (!string.IsNullOrWhiteSpace(user.PhoneNumber))
                claims.Add(new Claim("phone_number", user.PhoneNumber));

            claims.AddRange(userClaims);
            claims.AddRange(roleClaims);


            var symmetricSecurityKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_jwt.Key));
            var signingCredentials = new SigningCredentials(symmetricSecurityKey, SecurityAlgorithms.HmacSha256);

            var jwtSecurityToken = new JwtSecurityToken(
                issuer: _jwt.Issuer,
                audience: _jwt.Audience,
                claims: claims,
                expires: DateTime.Now.AddDays(Convert.ToDouble(_jwt.DurationInDays)),
                signingCredentials: signingCredentials
            );

            return jwtSecurityToken;
        }


    }
}