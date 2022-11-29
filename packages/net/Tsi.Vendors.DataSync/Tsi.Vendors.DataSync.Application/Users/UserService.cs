using Tsi.Vendors.DataSync.Domain.Entities.Users;
using Tsi.Vendors.DataSync.Domain.Interfaces;

namespace Tsi.Vendors.DataSync.Application.Users
{
    public class UserService : IUserService
    {
        private readonly IUnitOfWork _unitOfWork;

        public UserService(IUnitOfWork unitOfWork)
        {
            _unitOfWork = unitOfWork;
        }

        public async Task<List<UserDto>> ListUsersAsync()
        {
            var userRepository = _unitOfWork.Repository<User>();
            var users = await userRepository.ListAsync(x => true);
            
            return users.ToUserDtos();
        }

        public async Task<UserDto?> GetUserByIdAsync(int id)
        {
            var userRepository = _unitOfWork.Repository<User>();
            var user = await userRepository.GetAsync(x => x.Id == id);

            return user?.ToUserDto();
        }
    }
}
