using DSD_WinformsApp.Core.DTOs;

namespace DSD_WinformsApp.Infrastructure.Data.Services
{
    public interface IUserRepository
    {
        Task<List<UserCredentialsDto>> GetAllUsers();
        Task<List<UserCredentialsDto>> GetAllUsers(UserRole role);
        void RegisterUser(UserCredentialsDto userCredentials);
        Task<UserCredentialsDto?> GetUserByUserName(string userName);
        string GetUserRole(string emailAddress);
        Task<List<UserCredentialsDto>> GetFilteredUsers(string searchUserQuery, string jobFilterCategory);
        Task<bool> EditUser(int userId, UserCredentialsDto userCredentials);
        Task<bool> DeleteUser(int userId);

    }
}