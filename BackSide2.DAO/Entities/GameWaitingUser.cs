namespace Auga.DAO.Entities
{
    public class GameWaitingUser : BaseEntity
    {
        public long UserId { get; set; }
        public long ItemId { get; set; }
        public long Username { get; set; }
    }
}