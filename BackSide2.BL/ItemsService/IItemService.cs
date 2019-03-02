using System.Collections.Generic;
using System.Threading.Tasks;
using Auga.BL.Models.BoardDto;
using Auga.DAO.Entities;

namespace Auga.BL.ItemsService
{
    public interface IItemService
    {
        Task<Item> AddItemAsync(AddItemDto model);

        Task<Item> DeleteItemAsync(DeleteBoardDto model);

        //Task<BoardReturnDto> UpdateItemAsync(
        //    UpdateBoardDto model
        //);

        Task<Item> GetItemAsync(int itemId);

        Task<List<Item>> GetItemAsync(long userId);

        //Task<Item> GetItemAsync(string userNickname);

        Task<List<Item>> GetItemsAsync();

        Task<List<Item>> GetOwnItemsAsync();
    }
}