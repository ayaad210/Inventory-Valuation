using businessLayer.Dtos.Item;
using businessLayer.Dtos.Order;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace businessLayer.IServices
{
    public interface IOrderService
    {
       public Task<OrderCreateDto> CreateOrder(OrderCreateDto ItemCDto);
    }
}
