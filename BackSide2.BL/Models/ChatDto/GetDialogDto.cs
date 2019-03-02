using System;

namespace Auga.BL.Models.ChatDto
{
    class GetDialogDto
    {
        public string RecipientUserName { get; set; }
        public long RecipientId { get; set; }
        public bool Unread { get; set; }
        public DateTime LastChange { get; set; }

        //public string MessageContent { get; set; }
    }
}
