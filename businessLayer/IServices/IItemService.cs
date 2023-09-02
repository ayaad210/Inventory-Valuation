using businessLayer.Dtos.Item;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace businessLayer.IServices
{
    public interface IItemService 
    {

        public Task<ItemReadDto> CreateItem(ItemCreateDto ItemCDto);
        public Task<IEnumerable<ItemReadDto>> GetItems();

        public Task<ItemReadDto> GetItemById(int Id);

    }
}
