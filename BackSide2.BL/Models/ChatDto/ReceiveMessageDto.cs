using System;

namespace Auga.BL.Models.ChatDto
{
    public class ReceiveMessageDto
    {
        public string SenderUserName { get; set; }
        public long SenderId { get; set; }
        public bool Unread { get; set; }
        public DateTime LastChange { get; set; }
        public string MessageContent { get; set; }
    }
}