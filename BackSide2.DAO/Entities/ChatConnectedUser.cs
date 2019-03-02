namespace Auga.DAO.Entities
{
    public class ChatConnectedUser : BaseEntity
    {
        public long UserId { get; set; }
        public string ConnectionId { get; set; }
    }
}


