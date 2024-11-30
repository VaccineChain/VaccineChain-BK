using vaccine_chain_bk.DTO.User;

namespace vaccine_chain_bk.Services.Users
{
    public interface IUserService
    {
        string Register(RegisterDto registerDto);
        Task<AuthResponse> Login(LoginDto loginDto);
        string ChangePassword(string email, ChangePasswordDto changePasswordDto);
        UserDto GetProfile(string email);
        UserDto UpdateProfile(string email, UpdateUserDto updateUserDto);
    }
}
