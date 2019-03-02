using System.Threading.Tasks;
using Auga.BL.Models.AuthorizeDto;

namespace Auga.BL.Authorize
{
    public interface ITokenService 
    {
        Task<LoggedDto> RegisterAsync(RegisterDto entity);
        Task<LoggedDto> LoginAsync(LoginDto entity);
    }
}