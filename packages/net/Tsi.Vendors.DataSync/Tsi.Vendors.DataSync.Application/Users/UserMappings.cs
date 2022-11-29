using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Tsi.Vendors.DataSync.Application.Calendars;
using Tsi.Vendors.DataSync.Application.History;
using Tsi.Vendors.DataSync.Application.MailBoxes;
using Tsi.Vendors.DataSync.Domain.Base;
using Tsi.Vendors.DataSync.Domain.Entities.Users;

namespace Tsi.Vendors.DataSync.Application.Users
{
    internal static class UserMappings
    {
        public static UserDto ToUserDto(this User user)
        {
            return new UserDto
            {
                Id = user.Id,
                UserName = user.UserName,
                FirstName = user.FirstName,
                LastName = user.LastName,
                BirthDate = user.BirthDate,
                Address = user.EmailAddress,
                MailBoxes = user.MailBoxes?.ToList().ToMailBoxDtos(),
                Calendars = user.Calendars?.ToList().ToCalendarDtos()
            };
        }

        public static User ToUser(this UserDto userDto)
        {
            if (userDto.Id == null)
            {
                return new User(
                    userDto.UserName, 
                    userDto.FirstName, 
                    userDto.LastName, 
                    userDto.BirthDate,
                    userDto.CreatedDate, 
                    userDto.LastModifiedDate);
            }
            
            return new User(
                userDto.Id.Value, 
                userDto.UserName, 
                userDto.FirstName, 
                userDto.LastName, 
                userDto.BirthDate,
                userDto.CreatedDate, 
                userDto.LastModifiedDate);

        }

        internal static List<UserDto> ToUserDtos(this List<User> users)
        {
            return users?.Select(ToUserDto)?.ToList() ?? new List<UserDto>();
        }

        internal static List<User> ToUsers(this List<UserDto> userDtos)
        {
            return userDtos?.Select(ToUser)?.ToList() ?? new List<User>();
        }
    }
}
