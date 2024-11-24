using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Text;
using vaccine_chain_bk.Constraints;
using vaccine_chain_bk.DTO.User;
using vaccine_chain_bk.Exceptions;
using vaccine_chain_bk.Models;
using vaccine_chain_bk.Repositories.Does;
using vaccine_chain_bk.Repositories.Users;
using vaccine_chain_bk.Repositories.Roles;
using NuGet.Common;
using System.Net.Http.Headers;
using System.Net.Http;
using System.Text.Json;
using vaccine_chain_bk.DTO.Sensor;
using Azure.Core;

namespace vaccine_chain_bk.Services.Users
{
    public class UserService : IUserService
    {
        private readonly IMapper _mapper;
        private readonly IUserRepository _userRepository;
        private readonly IRoleRepository _roleRepository;
        private readonly HttpClient _httpClient;

        public UserService(IMapper mapper, IUserRepository userRepository, IRoleRepository roleRepository, HttpClient httpClient)

        {
            _mapper = mapper;
            _userRepository = userRepository;
            _roleRepository = roleRepository;
            _httpClient = httpClient;
        }

        public string Register(RegisterDto registerDto)
        {
            // Get user by email
            User user = _userRepository.GetUserByEmail(registerDto.Email);
            if (user != null)
            {
                throw new ConflictException("Email already exists");
            }

            // Hash password
            var passwordHasher = new PasswordHasher<string>();
            registerDto.Password = passwordHasher.HashPassword(null, registerDto.Password);

            // Map registerDto to user
            user = _mapper.Map<User>(registerDto);

            // Get role by name
            Role role = _roleRepository.GetRoleByName(ERole.User.ToString()) ?? throw new Exception("Role not found");
            user.RoleId = role.RoleId;

            // Create user
            _userRepository.CreateUser(user);

            return "Register success!";
        }

        public async Task<AuthResponse> Login(LoginDto loginDto)
        {
            // Get user by email
            User user = _userRepository.GetUserByEmail(loginDto.Email) ?? throw new AuthenticationException("Invalid Credentials!");

            // Verify password
            var passwordHasher = new PasswordHasher<string>();
            if (passwordHasher.VerifyHashedPassword(null, user.Password, loginDto.Password) == PasswordVerificationResult.Failed)
            {
                throw new AuthenticationException("Invalid Credentials!");
            }

            string url = Environment.GetEnvironmentVariable("NODE_VACCINE_URL");

            UserTokenRequest request = new UserTokenRequest
            {
                Username = loginDto.Email,
                OrgName = "Org1"
            };

            StringContent jsonContent = new StringContent(JsonSerializer.Serialize(request), Encoding.UTF8, "application/json");
            HttpResponseMessage response = await _httpClient.PostAsync($"{url}/users", jsonContent);

            if (!response.IsSuccessStatusCode)
            {
                throw new AuthenticationException("Invalid Credentials!");
            }

            var responseBody = await response.Content.ReadAsStringAsync();
            var authResponse = JsonSerializer.Deserialize<AuthResponse>(responseBody, new JsonSerializerOptions
            {
                PropertyNameCaseInsensitive = true
            });

            return authResponse ?? throw new AuthenticationException("Invalid Credentials!");
        }
    }
}
