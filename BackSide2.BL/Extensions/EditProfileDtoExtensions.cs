using Auga.BL.Models.ProfileDto;
using Auga.DAO.Entities;

namespace Auga.BL.Extensions
{
    public static class EditProfileDtoExtensions
    {
        public static User ToPerson(this EditProfileDto model, User user)
        {
            user.Email = model.Email;
            user.UserName = model.Username;
            user.UpdatedBy = user.Id;

            return user;
        }
    }
}