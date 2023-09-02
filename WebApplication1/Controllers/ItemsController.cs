using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using businessLayer.Dtos.Item;
using businessLayer.IServices;
using DataAccessLayer;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;

namespace WebApplication1
{
    [Authorize(Roles ="admin")]
    public class ItemsController : Controller
    {
        private readonly IItemService _ItemService;

        public ItemsController(IItemService ItemServiceItem)
        {
            _ItemService = ItemServiceItem;
        }

        // GET: Items
        public async Task<IActionResult> Index()

        {
            var res = await  _ItemService.GetItems();
            List <ItemReadDto> list = res.ToList();

            return                      View(list);
                      //list.Count>0  ?  Problem("Entity set 'WebApplication1ItemService.Item'  is null.");
        }


        // GET: Items/Create
        public IActionResult Create()
        {
            return View(new ItemCreateDto());
        }


        [HttpPost]
        [ValidateAntiForgeryToken]

        public async Task<IActionResult> Create([Bind(include: "Name")] ItemCreateDto itemdDTO)
        {
            if (ModelState.IsValid)
            {
                try
                {
                    await _ItemService.CreateItem(itemdDTO);

                    return RedirectToAction(nameof(Index));
                }
                catch (Exception)
                {

                    return RedirectToAction("BadRequest", "Default");
                }
            }
            return View(itemdDTO);
        }




    }
}
