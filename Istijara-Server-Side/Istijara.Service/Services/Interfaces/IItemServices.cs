
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Istijara.Repository.Dtos;

namespace Istijara.Service.Services.Interfaces
{
    public interface IItemServices
    {


        Task<ItemDto> CreateItemAsync(ItemDto itemDto);

        Task<ItemDto> UpdateItemAsync(Guid id, ItemDto dto);
        Task<bool> DeleteItemAsync(Guid id);

        Task<IReadOnlyList<ItemDto>> GetAllItemsAsync();
        Task<ItemDto> GetItemByIdAsync(Guid id);




    }
}
