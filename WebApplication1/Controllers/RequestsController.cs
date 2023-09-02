using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using businessLayer.Dtos.Request;
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

    public class RequestsController : Controller
    {
        private readonly IRequestService _requestService;
        private readonly IItemService _itemService;

        public RequestsController(IRequestService RequestServiceRequest,IItemService itemService)
        {
            _requestService = RequestServiceRequest;
            this._itemService = itemService;
        }

        // GET: Requests
        //public async Task<IActionResult> Index()

        //{
        //    List <RequestReadDto> list = _requestService.GetRequests().ToList();

        //    return                      View(list);
        //              //list.Count>0  ?  Problem("Entity set 'WebApplication1RequestService.Request'  is null.");
        //}


        // GET: Requests/Create
        public async Task<IActionResult> Create()
        {
            var items = await _itemService.GetItems();

            ViewBag.Items = new SelectList(items.ToList(), "Id", "Name");



            return View(new RequestCreateDto());
        }
 






        [HttpPost]
        [ValidateAntiForgeryToken]
        public async Task<IActionResult> Create([Bind(include: "ItemId,count,price")] RequestCreateDto RequestdDTO)
        {
            try
            {
                if (ModelState.IsValid)
                {
                    await _requestService.CreateRequest(RequestdDTO);

                    return RedirectToAction("Success", "Default");
                }

            }
            catch (Exception)
            {
                return RedirectToAction("BadRequest", "Default");


            }
            var items = await _itemService.GetItems();

            ViewBag.Items = new SelectList(items.ToList(), "Id", "Name",RequestdDTO.ItemId);
            return View(RequestdDTO);
        }




    }
}
