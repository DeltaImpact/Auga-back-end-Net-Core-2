using System.ComponentModel.DataAnnotations;

namespace Auga.BL.Models.BoardDto
{
    public class DeleteBoardDto
    {
        [Required]
        public long Id { get; set; }
    }
}
