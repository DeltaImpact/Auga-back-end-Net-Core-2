using System;
using System.Threading.Tasks;
using Auga.BL.ItemsService;
using Auga.BL.Models.BoardDto;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace Auga.Controllers
{
    [Route("items")]
    [ApiController]
    public class ItemsController : Controller
    {
        private readonly IItemService _itemService;

        public ItemsController(IItemService itemService)
        {
            _itemService = itemService;
        }

        /// <summary>
        /// Retrieves all items
        /// </summary>
        [HttpGet]
        public async Task<IActionResult> GetItems()
        {
            try
            {
                var responsePayload = await _itemService.GetItemsAsync();
                return Ok(responsePayload);
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// Retrieves a specific item by unique id
        /// </summary>
        [HttpGet("{id}")]
        public async Task<IActionResult> Get(int id)
        {
            try
            {
                var responsePayload = await _itemService.GetItemAsync(id);
                return Ok(responsePayload);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(new {ex.Message});
            }
        }

        /// <summary>
        /// Retrieves a user items by user id
        /// </summary>
        [HttpGet("{userId}")]
        public async Task<IActionResult> Get(long userId)
        {
            try
            {
                var responsePayload = await _itemService.GetItemAsync(userId);
                return Ok(responsePayload);
            }
            catch (UnauthorizedAccessException)
            {
                return Unauthorized();
            }
            catch (Exception ex)
            {
                return BadRequest(new { ex.Message });
            }
        }

        /// <summary>
        /// Creates new item
        /// </summary>
        [Authorize]
        [HttpPost("item")]
        public async Task<IActionResult> AddItem(
            AddItemDto model
        )
        {
            try
            {
                var responsePayload = await _itemService.AddItemAsync(model);
                return Ok(responsePayload);
            }
            catch (Exception ex)
            {
                return BadRequest(new {ex.Message});
            }
        }

        /// <summary>
        /// Deletes a specific user by unique id
        /// </summary>
        [Authorize]
        [HttpDelete("item")]
        public async Task<IActionResult> DeleteItem(
            DeleteBoardDto model
        )
        {
            try
            {
                var responsePayload = await _itemService.DeleteItemAsync(model);
                return Ok(responsePayload);
            }
            catch (Exception ex)
            {
                return BadRequest(new {ex.Message});
            }
        }

        //[Authorize]
        //[HttpPost("updateBoard")]
        //public async Task<IActionResult> UpdateBoard(
        //    UpdateBoardDto model
        //)
        //{
        //    try
        //    {
        //        var responsePayload = await _itemService.UpdateBoardAsync(model);
        //        return Ok(responsePayload);
        //    }
        //    catch (Exception ex)
        //    {
        //        return BadRequest(new {ex.Message});
        //    }
        //}

    }
}