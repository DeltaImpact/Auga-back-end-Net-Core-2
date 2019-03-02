using System.ComponentModel.DataAnnotations;

namespace Auga.BL.Models.BoardPinDto
{
    public class DeletePinFromBoardDto
    {
        [Required] public long PinId { get; set; }

        [Required] public long BoardId { get; set; }
    }
}