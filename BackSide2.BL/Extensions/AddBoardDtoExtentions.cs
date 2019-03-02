using Auga.BL.Models.BoardDto;
using Auga.DAO.Entities;

namespace Auga.BL.Extensions
{
    public static class AddBoardDtoExtentions
    {
        //public static Item ToItem(this AddItemDto model, long personId)
        //{
        //    var board = new Item
        //    {
        //        Name = model.Name,
        //        Description = model.Description,
        //        Img = model.Img,
        //        IsPrivate = model.IsPrivate,
        //        CreatedBy = personId,
        //        User = {Id = personId}
        //    };
        //    return board;
        //}

        public static Item ToItem(this AddItemDto model, long sellerId)
        {
            var item = new Item
            {
                Name = model.Name,
                Description = model.Description,
                Cost = model.Cost,
                NumberOfParticipants = model.ParticipantsNumber,
                CreatedBy = sellerId,
                //Seller = {Id = sellerId}
            };
            return item;
        }

        //public static Item ToItem(this AddItemDto model)
        //{
        //    var board = new Item
        //    {
        //        Name = model.Name,
        //        Description = model.МDescription,
        //        Img = model.Img,
        //        IsPrivate = model.IsPrivate,
        //    };
        //    return board;
        //}
    }
}