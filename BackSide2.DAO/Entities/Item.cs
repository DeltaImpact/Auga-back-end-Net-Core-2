namespace Auga.DAO.Entities
{
    public class Item : BaseEntity
    {
        public string Name { get; set; }
        public string Description { get; set; }
        public double Cost { get; set; }
        public long NumberOfParticipants { get; set; }
        public User Buyer { get; set; }
        public User Seller { get; set; }
    }
}