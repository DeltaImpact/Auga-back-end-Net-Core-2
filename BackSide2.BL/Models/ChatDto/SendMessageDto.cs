using System.ComponentModel.DataAnnotations;

namespace Auga.BL.Models.ChatDto
{
    public class SendMessageDto
    {
        [Required]
        [StringLength(2000, ErrorMessage = "{0} must be between {2} and {1} characters long.", MinimumLength = 1)]
        public string MessageContent { get; set; }

        [Required] public long SentToId { get; set; }
    }
}