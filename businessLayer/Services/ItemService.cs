using AutoMapper;
using businessLayer.Dtos.Item;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using DataAccessLayer.UnitOfWork_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using businessLayer.exeptions;
using Microsoft.Extensions.Logging;
using System.Text.Json;

namespace businessLayer.IServices
{
    public class ItemService : IItemService
    {
        private readonly IItemRepo itemRepo;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<ItemService> logger;

        public ItemService( IUnitOfWork unitOfWork, IMapper mapper,  ILogger<ItemService> logger)
        {
            this.itemRepo = unitOfWork.ItemRepository();
            this.unitOfWork = unitOfWork;
            this._mapper = mapper;
            this.logger = logger;
        }
        public async Task<ItemReadDto> CreateItem(ItemCreateDto ItemCDto)
        {

            try
            {
                Item item = _mapper.Map<Item>(ItemCDto);
                item.TotalCount=0;
                item.CreatedDate=DateTime.Now;
                var res = await this.itemRepo.Insert(item);

                bool success =     await unitOfWork.Save();
                ItemReadDto ResDto = _mapper.Map<ItemReadDto>(res);

                // ex of Debug Log
                 string Logs=  JsonSerializer.Serialize(ItemCDto);
                 logger.LogDebug(Logs);
         

                if (!success)
                {
                    throw new CanNotCreateExeption(" Can Not Create", "Item");

                }

                return ResDto;

            }
            catch (Exception ex )
            {
                logger.LogError(ex, "Can Not Create Order {Message}", ex.Message);

                throw;
            }

        }

        public async Task<ItemReadDto> GetItemById(int Id)
        {
            try
            {
                Item item = await itemRepo.GetByID(Id);


                var res = _mapper.Map<ItemReadDto>(item);


                return res;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Can Not fINDE Order {Message}", ex.Message);

                throw;
            }

        }

        public async Task<IEnumerable<ItemReadDto>> GetItems()
        {
            var items = await itemRepo.Get();
            IEnumerable<ItemReadDto> res = _mapper.Map<IEnumerable<ItemReadDto>>(items);

            return res;
        }
    }
}
