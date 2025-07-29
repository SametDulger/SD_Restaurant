using SD_Restaurant.Application.DTOs;
using SD_Restaurant.Core.Entities;

namespace SD_Restaurant.Application.Services
{
    public interface IAuthService
    {
        Task<AuthResponseDto> LoginAsync(LoginDto loginDto);
        Task<AuthResponseDto> RegisterAsync(RegisterDto registerDto);
        Task<AuthResponseDto> RefreshTokenAsync(string refreshToken);
        Task<bool> ValidateTokenAsync(string token);
        Task<bool> RevokeTokenAsync(string refreshToken);
        Task<bool> ChangePasswordAsync(ChangePasswordDto changePasswordDto);
        Task<User?> GetUserByUsernameAsync(string username);
    }
} 