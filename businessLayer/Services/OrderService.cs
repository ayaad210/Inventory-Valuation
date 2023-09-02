using AutoMapper;
using businessLayer.Dtos.Item;
using businessLayer.Dtos.Order;
using businessLayer.exeptions;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models;
using DataAccessLayer.Repositories;
using DataAccessLayer.UnitOfWork_;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Text.Json.Nodes;
using System.Threading.Tasks;

namespace businessLayer.IServices
{
    public class OrderService : IOrderService
    {
        private readonly IOrderRepo orderRepo;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<OrderService> logger;
        private readonly IItemRepo  itemRepo;

        public OrderService(IUnitOfWork unitOfWork, IMapper mapper ,ILogger<OrderService>  logger)
        {
            this.orderRepo = unitOfWork.OrderRepository();
            this.unitOfWork = unitOfWork;
            this._mapper = mapper;
            this.logger = logger;
            itemRepo = unitOfWork.ItemRepository();

        }

        public async Task<OrderCreateDto> CreateOrder(OrderCreateDto OrderDto)
        {
            try
            {

                Order order = _mapper.Map<Order>(OrderDto);

                var res = await this.orderRepo.Insert(order);

                OrderCreateDto OrderCreateDto = _mapper.Map<OrderCreateDto>(res);


                Item item = await itemRepo.GetByID(OrderDto.ItemId);

                if (item.TotalCount<OrderDto.count)
                {
                   throw new OrderExceedItemExeption("Order Count Exceed Item Count");
                }

                item.TotalCount = item.TotalCount - OrderDto.count;
                await itemRepo.Update(item);

                // logger.LOG();
                bool success = await unitOfWork.Save();

                if (!success)
                {

                    throw new CanNotCreateExeption("Can Not Create Order","Orders");

                }

                return OrderCreateDto;

            }
            catch (Exception ex)
            {
            await    unitOfWork.RollBackChangesAsync();
                logger.LogError(ex, "Can Not Create Order {Message}", ex.Message);
                throw;
            }

        }


        //public IEnumerable<ItemReadDto> GetItems()
        //{
        //    var items = itemRepo.Get();
        //    var res = _mapper.Map<IEnumerable<ItemReadDto>>(items);

        //    return res;
        //}
    }


    
}
