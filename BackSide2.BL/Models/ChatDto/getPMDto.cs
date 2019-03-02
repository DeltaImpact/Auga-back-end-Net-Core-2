using System.ComponentModel.DataAnnotations;

namespace Auga.BL.Models.ChatDto
{
    public class GetPmDto
    {
        [Required] public string Message { get; set; }
        [Required] public long SentTo { get; set; }

    }
}