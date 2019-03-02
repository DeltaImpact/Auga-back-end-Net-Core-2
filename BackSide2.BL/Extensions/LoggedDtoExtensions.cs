using Auga.BL.Models.AuthorizeDto;
using Auga.DAO.Entities;

namespace Auga.BL.Extensions
{
    public static class LoggedDtoExtensions
    {
        public static LoggedDto ToLoggedDto(this User user, string token)
        {
            return new LoggedDto
            {
                UserName = user.UserName,
                Email = user.Email,
                Token = token,
            };
        }

        public static LoggedDto ToLoggedDto(this User user)
        {
            return new LoggedDto
            {
                UserName = user.UserName,
                Email = user.Email,
            };
        }
    }
}
