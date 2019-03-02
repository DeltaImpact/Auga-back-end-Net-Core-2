using Auga.BL.Models.AuthorizeDto;
using Auga.DAO.Entities;

namespace Auga.BL.Extensions
{
    public static class RegisterDtoExtensions
    {
        public static User ToPerson(this RegisterDto model, User createdBy)
        {
            var person = new User
            {
                UserName = model.Username,
                Email = model.Email,
                CreatedBy = createdBy.Id,
                UpdatedBy = null,
            };
            return person;
        }

    }
}