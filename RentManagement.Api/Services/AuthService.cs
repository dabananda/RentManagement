using Microsoft.AspNetCore.Identity;
using RentManagement.Api.Constants;
using RentManagement.Api.DTOs;
using RentManagement.Api.Interfaces;
using RentManagement.Api.Models;

namespace RentManagement.Api.Services
{
    public class AuthService : IAuthService
    {
        private readonly UserManager<ApplicationUser> _userManager;
        private readonly RoleManager<IdentityRole> _roleManager;
        private readonly SignInManager<ApplicationUser> _signInManager;
        private readonly IEmailSender _emailSender;
        private readonly IJwtService _jwtService;
        private readonly IConfiguration _configuration;

        public AuthService(
            UserManager<ApplicationUser> userManager,
            RoleManager<IdentityRole> roleManager,
            SignInManager<ApplicationUser> signInManager,
            IEmailSender emailSender,
            IJwtService jwtService,
            IConfiguration configuration)
        {
            _userManager = userManager;
            _roleManager = roleManager;
            _signInManager = signInManager;
            _emailSender = emailSender;
            _jwtService = jwtService;
            _configuration = configuration;
        }

        public async Task<Tuple<bool, string>> RegisterUserAsync(RegisterDto model)
        {
            var userExists = await _userManager.FindByEmailAsync(model.Email);
            if (userExists != null)
            {
                return new Tuple<bool, string>(false, "User with this email already exists.");
            }

            var user = new ApplicationUser
            {
                Email = model.Email,
                UserName = model.Email,
                SecurityStamp = Guid.NewGuid().ToString(),
                EmailConfirmed = false
            };

            var result = await _userManager.CreateAsync(user, model.Password);

            if (!result.Succeeded)
            {
                var errors = string.Join(" | ", result.Errors.Select(e => e.Description));
                return new Tuple<bool, string>(false, $"User creation failed: {errors}");
            }

            await _userManager.AddToRoleAsync(user, UserRoles.Owner);

            var token = await _userManager.GenerateEmailConfirmationTokenAsync(user);

            string confirmationLink = $"http://localhost:4200/auth/confirm-email?userId={user.Id}&token={Uri.EscapeDataString(token)}";

            string body = $@"
                <p>Please confirm your account by clicking the link below:</p>
                <p><a href='{confirmationLink}'>Confirm Email</a></p>
                <p>If the link doesn’t work, copy and paste this into your browser:</p>
                <p>{confirmationLink}</p>
            ";

            await _emailSender.SendEmailAsync(user.Email, "Confirm your email", body);


            return new Tuple<bool, string>(true, "User created successfully. Please confirm your email.");
        }

        public async Task<LoginResponseDto?> LoginUserAsync(LoginDto model)
        {
            var user = await _userManager.FindByEmailAsync(model.Email);
            if (user is null) return null;

            var signInResult = await _signInManager.PasswordSignInAsync(
                userName: user.UserName!,
                password: model.Password,
                isPersistent: false,
                lockoutOnFailure: true);

            if (!signInResult.Succeeded) return null;
            if (!await _userManager.IsEmailConfirmedAsync(user)) return null;

            var roles = await _userManager.GetRolesAsync(user);
            var token = _jwtService.GenerateToken(user.Id!, user.UserName!, roles);

            return new LoginResponseDto
            {
                Email = user.Email!,
                Token = token,
                Roles = [..roles]
            };
        }

        public async Task<bool> ConfirmEmailAsync(string userId, string token)
        {
            if (string.IsNullOrEmpty(userId) || string.IsNullOrEmpty(token))
            {
                return false;
            }

            var user = await _userManager.FindByIdAsync(userId);
            if (user == null)
            {
                return false;
            }

            var decodedToken = Uri.UnescapeDataString(token);

            var result = await _userManager.ConfirmEmailAsync(user, decodedToken);

            return result.Succeeded;
        }
    }
}