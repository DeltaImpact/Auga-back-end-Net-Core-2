using System.Threading.Tasks;
using Auga.BL.Models.AuthorizeDto;
using Auga.BL.Models.ProfileDto;

namespace Auga.BL.ProfileService
{
    public interface IProfileService
    {
        Task<ProfileReturnDto> GetUserProfileInfo(string userNickname);
        Task<ProfileReturnDto> GetUserProfileInfo();
        Task<LoggedDto> ChangeProfileAsync(EditProfileDto model);
        //Task ChangePasswordAsync(ChangePasswordDto model);
    }
}