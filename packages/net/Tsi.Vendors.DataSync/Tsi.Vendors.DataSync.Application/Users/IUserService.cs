using System.Linq.Expressions;
using Tsi.Vendors.DataSync.Domain.Entities.Users;

namespace Tsi.Vendors.DataSync.Application.Users
{
    public interface IUserService
    {
        Task<List<UserDto>> ListUsersAsync();
        Task<UserDto?> GetUserByIdAsync(int id);
    }
}
