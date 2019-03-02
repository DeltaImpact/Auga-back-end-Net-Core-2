using System.ComponentModel.DataAnnotations;

namespace Auga.BL.Models.PinDto
{
    public class DeletePinDto
    {
        [Required]
        public long Id { get; set; }
    }
}
