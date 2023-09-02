using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using businessLayer.Dtos.Item;
using businessLayer.Dtos.Order;
using businessLayer.IServices;
using DataAccessLayer;
using DataAccessLayer.Models;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1
{
    [Authorize(Roles = "Employee")]

    public class OrdersController : Controller
    {
        private readonly IOrderService _orderService;
        private readonly IItemService _itemService;

        public OrdersController(IOrderService ordrServiceItem,IItemService itemService )
        {
            _orderService = ordrServiceItem;
            this._itemService = itemService;
        }

        // GET: Items
        //public async Task<IActionResult> Index()

        //{
        //    List <ItemReadDto> list = _orderService.GetItems().ToList();

        //    return                      View(list);
        //              //list.Count>0  ?  Problem("Entity set 'WebApplication1ItemService.Item'  is null.");
        //}


        // GET: Items/Create
        public async Task<IActionResult>   Create()
        {
            var items = await _itemService.GetItems();

            ViewBag.Items = new SelectList(items.ToList(), "Id", "Name");

            return View(new OrderCreateDto());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind(include: "ItemId,count,price")] OrderCreateDto orderDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    ItemReadDto item = await this._itemService.GetItemById(orderDTO.ItemId);

                    if (item.TotalCount >= orderDTO.count)
                    {
                        await _orderService.CreateOrder(orderDTO);

                        return RedirectToAction("Success", "Default");
                    }

                }
                catch (Exception)
                {

                    return RedirectToAction("BadRequest", "Default");

                }


            }
            var items = await _itemService.GetItems();
            ViewBag.Items = new SelectList(items.ToList(), "Id", "Name",orderDTO.ItemId);

            return View(orderDTO);
        }

        [HttpPost]
        [AllowAnonymous]
        public async Task<decimal> GetTotalCountOfItem([FromBody] OrderCreateDto ord)
        {

            try
            {
                if (ord != null && ord.ItemId > 0)
                {
                    ItemReadDto item = await this._itemService.GetItemById(ord.ItemId);
                    return item.TotalCount;

                }
                return 0;
            }
            catch (Exception)
            {

                return 0;
            }

        }


    }
}
