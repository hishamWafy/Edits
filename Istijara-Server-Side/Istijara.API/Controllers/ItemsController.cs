using Istijara.API.Dtos;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Istijara.Service.Services.Interfaces;
using Istijara.Repository.Dtos;
using Istijara.Service.Services;


namespace Istijara.API.Controllers
{

    public class ItemsController : BaseApiController
    {
        private readonly IItemServices _itemService;
        public ItemsController(IItemServices itemServices)
        {
            _itemService = itemServices;
        }


        [HttpPost("AddItem")]
        public async Task<IActionResult> AddItem([FromBody] ItemDto item)
        {
            var result = await _itemService.CreateItemAsync(item);
            return Ok(result);
        }



        [HttpPut("UpdateItem/{id}")]
        public async Task<IActionResult> UpdateItem(Guid id, ItemDto itemDto)
        {
            try
            {
                var updatedItem = await _itemService.UpdateItemAsync(id, itemDto);
                return Ok(updatedItem);
            }
            catch (KeyNotFoundException ex)
            {
                return NotFound(ex.Message);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpDelete("DeleteItem/{id}")]
        public async Task<IActionResult> DeleteItem(Guid id)
        {
            var success = await _itemService.DeleteItemAsync(id);
            if (!success)
                return NotFound($"Item with ID {id} not found.");

            return NoContent();
        }


        [HttpGet]
        public async Task<IActionResult> GetAllItems()
        {
            try
            {
                var items = await _itemService.GetAllItemsAsync();
                return Ok(items);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetItemById(Guid id)
        {
            try
            {
                var item = await _itemService.GetItemByIdAsync(id);
                if (item == null)
                    return NotFound($"Item with ID {id} not found.");
                return Ok(item);
            }
            catch (Exception ex)
            {
                return StatusCode(500, ex.Message);
            }



        }
    }
}
