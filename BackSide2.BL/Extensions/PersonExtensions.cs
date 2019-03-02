using Auga.BL.Models.ProfileDto;
using Auga.DAO.Entities;

namespace Auga.BL.Extensions
{
    public static class PersonExtensions
    {
        public static ProfileReturnDto ToProfileOwnReturnDto(this User user, bool isOnline)
        {
            return new ProfileReturnDto
            {
                Id = user.Id,
                UserName = user.UserName,
                Email = user.Email,
                IsOnline = isOnline,
            };
        }

        public static ProfileReturnDto ToProfileReturnDto(this User user)
        {
            return new ProfileReturnDto
            {
                Id = user.Id,
                UserName = user.UserName
            };
        }

        public static ProfileReturnDto ToProfileReturnDto(this User user, bool isOnline)
        {
            return new ProfileReturnDto
            {
                Id = user.Id,
                UserName = user.UserName,
                IsOnline = isOnline,
            };
        }
    }
}