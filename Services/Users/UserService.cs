using AutoMapper;
using Microsoft.AspNetCore.Identity;
using System.Text;
using vaccine_chain_bk.Constraints;
using vaccine_chain_bk.DTO.User;
using vaccine_chain_bk.Exceptions;
using vaccine_chain_bk.Models;
using vaccine_chain_bk.Repositories.Users;
using vaccine_chain_bk.Repositories.Roles;
using System.Text.Json;

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
            }) ?? throw new AuthenticationException("Invalid Credentials!");

            UserDto userDto = _mapper.Map<UserDto>(user);
            authResponse.User = userDto;

            return authResponse;
        }

        public string ChangePassword(string email, ChangePasswordDto changePasswordDto)
        {
            User user = _userRepository.GetUserByEmail(email) ?? throw new NotFoundException("User not found");

            if (!VerifyPassword(user.Password, changePasswordDto.CurrentPassword))
            {
                throw new InvalidException("Current password is incorrect");
            }

            user.Password = HashPassword(changePasswordDto.NewPassword);
            _userRepository.UpdateUser(user);

            return "Password changed successfully";
        }

        public bool VerifyPassword(string currentPassword, string oldPassword)
        {
            var passwordHasher = new PasswordHasher<string>();
            if (passwordHasher.VerifyHashedPassword(null, currentPassword, oldPassword) == PasswordVerificationResult.Failed)
            {
                return false;
            }
            return true;
        }

        public string HashPassword(string newPassword)
        {
            var passwordHasher = new PasswordHasher<string>();
            return passwordHasher.HashPassword(null, newPassword);
        }

        public UserDto GetProfile(string email)
        {
            User user = _userRepository.GetUserByEmail(email) ?? throw new NotFoundException("User not found");
            return _mapper.Map<User, UserDto>(user);
        }

        public UserDto UpdateProfile(string email, UpdateUserDto updateUserDto)
        {
            User user = _userRepository.GetUserByEmail(email) ?? throw new NotFoundException("User not found");

            user.FirstName = updateUserDto.FirstName;
            user.LastName = updateUserDto.LastName;
            user.Email = updateUserDto.Email;
            user.DateOfBirth = updateUserDto.DateOfBirth;
            user.Address = updateUserDto.Address;

            _userRepository.UpdateUser(user);

            return _mapper.Map<User, UserDto>(user);
        }
    }
}
