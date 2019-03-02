using System.ComponentModel.DataAnnotations;

namespace Auga.BL.Models.BoardDto
{
    public class AddItemDto
    {
        [Required]
        [StringLength(100, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 3)]
        public string Name { get; set; }

        [StringLength(500, ErrorMessage = "{0} should be shorter than {2} symbols.")]
        public string Description { get; set; }

        [Required] public double Cost { get; set; }

        [Required] public long ParticipantsNumber { get; set; }
    }
}