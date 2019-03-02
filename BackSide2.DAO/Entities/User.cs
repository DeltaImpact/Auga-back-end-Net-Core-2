using System.Collections.Generic;

namespace Auga.DAO.Entities
{
    public class User : BaseEntity
    {
        public string UserName { get; set; }
        public string Email { get; set; }
        public ICollection<Item> Items { get; set; }
    }
}