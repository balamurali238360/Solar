using SunTrack.API.Data.Models;
using SunTrack.API.ViewModels;
using SunTrack.API.ViewModels.Users;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace SunTrack.API.Services.Users
{
    public interface IUserService
    {
        Task<List<UserVM>> GetAllUsersAsync(SearchVM search);
        Task<bool> AddUserAsync(UserVM user);
        Task<bool> UpdateUserAsync(UserVM user);
        Task<bool> DeleteUserAsync(int id);
    }
}
