using Auga.BL.Models.BoardDto;
using Auga.DAO.Entities;

namespace Auga.BL.Extensions
{
    public static class UpdateBoardDtoExtensions
    {
        public static Item ToBoard(this UpdateBoardDto model, Item item, long modifiedBy)
        {
            item.Name = model.Name;
            item.Description = model.Description;
            item.UpdatedBy = modifiedBy;
            return item;
        }
    }
}