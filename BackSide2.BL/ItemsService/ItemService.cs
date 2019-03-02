using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;
using System.Threading.Tasks;
using Auga.BL.Exceptions;
using Auga.BL.Extensions;
using Auga.BL.Models.BoardDto;
using Auga.DAO.Entities;
using Auga.DAO.Repository;
using Microsoft.AspNetCore.Http;
using Microsoft.EntityFrameworkCore;

namespace Auga.BL.ItemsService
{
    public class ItemService : IItemService
    {
        private readonly IRepository<Item> _itemService;
        private readonly IRepository<User> _userService;
        private readonly IHttpContextAccessor _httpContextAccessor;

        public ItemService(
            IRepository<Item> itemService,
            IRepository<User> userService,
            IHttpContextAccessor httpContextAccessor)
        {
            _itemService = itemService;
            _userService = userService;
            _httpContextAccessor = httpContextAccessor;
        }

        public async Task<Item> AddItemAsync(AddItemDto model)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var itemToAdd = model.ToItem(userId);
            var item = (await _itemService.InsertAsync(itemToAdd));
            //var item = (await _itemService.InsertAsync(itemToAdd)).ToBoardReturnDto();
            return item;
        }


        public async Task<Item> DeleteItemAsync(DeleteBoardDto model)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var userInDb =
                await (await _itemService.GetAllAsync(d => d.Id == model.Id && d.Seller.Id == userId))
                    .FirstOrDefaultAsync();

            if (userInDb == null)
            {
                throw new ObjectNotFoundException("Item not found.");
            }

            //var item = (await _itemService.RemoveAsync(userInDb)).ToBoardReturnDto();
            var item = (await _itemService.RemoveAsync(userInDb));
            return item;
        }

        public async Task<Item> UpdateItemAsync(UpdateBoardDto model)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);
            var boardOld =
                await (await _itemService.GetAllAsync(d => d.Id == model.Id)).FirstOrDefaultAsync();
            if (boardOld == null)
            {
                throw new ObjectNotFoundException("Item not found.");
            }

            var boardWithSameName =
                await (await _itemService.GetAllAsync(d => d.Name == model.Name)).FirstOrDefaultAsync();
            if (boardWithSameName != null && model.Id != boardWithSameName.Id)
            {
                throw new ObjectAlreadyExistException("Item with same name already exist.");
            }

            var board =
                await _itemService.UpdateAsync(model.ToBoard(boardOld, userId));
            //return board.ToBoardReturnDto();
            return board;
        }

        public async Task<Item> GetItemAsync(int itemId)
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);


            var item =
                await (await _itemService.GetAllAsync(d => d.Id == itemId)).FirstOrDefaultAsync();


            if (item == null)
            {
                throw new ObjectNotFoundException("Item not found.");
            }

            return item;
        }

        public async Task<List<Item>> GetItemAsync(long userId)
        {
            //var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var item =
                await (await _itemService.GetAllAsync(d => d.Seller.Id == userId)).ToListAsync();


            //if (item == null)
            //{
            //    throw new ObjectNotFoundException("Item not found.");
            //}

            return item;
        }

        //public Task<Item> GetItemAsync(string userNickname)
        //{
        //    var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

        //    var items =
        //        (await _itemService.GetAllAsync(item => item.Seller.Id == userId))
        //        .OrderBy(board => board.Created)
        //        //.Select(o => o.ToBoardReturnDto(o.BoardPins == null ? 0 : o.BoardPins.Count, true))
        //        .ToList();
        //    return items;
        //}

        public async Task<List<Item>> GetItemsAsync()
        {
            var items =
                (await _itemService.GetAllAsync())
                .OrderBy(board => board.Created)
                //.Select(o => o.ToBoardReturnDto(o.BoardPins == null ? 0 : o.BoardPins.Count, true))
                .ToList();
            return items;
        }

        public async Task<List<Item>> GetOwnItemsAsync()
        {
            var userId = long.Parse(_httpContextAccessor.HttpContext.User.FindFirst(ClaimTypes.NameIdentifier).Value);

            var items =
                (await _itemService.GetAllAsync(item => item.Seller.Id == userId))
                .OrderBy(board => board.Created)
                //.Select(o => o.ToBoardReturnDto(o.BoardPins == null ? 0 : o.BoardPins.Count, true))
                .ToList();
            return items;
        }
    }
}