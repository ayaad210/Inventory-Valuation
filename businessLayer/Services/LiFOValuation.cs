using businessLayer.Dtos.InventoryEvaluation;
using businessLayer.IServices;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models;
using DataAccessLayer.UnitOfWork_;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using businessLayer.exeptions;
using AbusinessLayer.exeptions;

namespace businessLayer.Services
{
    public class LiFOValuation : IInvintoryValuationStrategy
    {
        private readonly IItemRepo itemRepo;
        private readonly IRequestRepo requestRepo;
        private readonly IOrderRepo orderRepo;
        private readonly IUnitOfWork unitOfWork;

        public LiFOValuation(IUnitOfWork unitOfWork)
        {
            this.unitOfWork = unitOfWork;

            this.itemRepo = unitOfWork.ItemRepository();
            this.requestRepo = unitOfWork.RequestRepository();
            this.orderRepo = unitOfWork.OrderRepository();
        }

        public async Task<Evaluation> EvaluateInventory()
        {

            try
            {
                IEnumerable<Item> items = await itemRepo.Get();
                decimal TotalProfit = 0;
                decimal totalMony = 0;
                decimal totalCost=0;
                foreach (var item in items)
                {
                    IEnumerable<Order> ItemOrders = await orderRepo.Get(x => x.ItemId == item.Id);
                    decimal ItemOrdersCount = ItemOrders.Sum(x => x.count);
                    decimal TotalProfitForIItem = ItemOrders.Sum(x => x.price);
                    decimal TotalCostPaidForItem = 0;

                    var ItemRequests = await requestRepo.Get(x => x.ItemId == item.Id, x => x.OrderByDescending(i => i.id));
                    foreach (var request in ItemRequests)
                    {
                        decimal CollectionPrice = 0;
                        if (ItemOrdersCount >= request.count)
                        {
                            CollectionPrice = request.price * request.count;
                            ItemOrdersCount = ItemOrdersCount - request.count;
                        }
                        else
                        {
                            CollectionPrice = request.price * ItemOrdersCount;
                            ItemOrdersCount = 0;
                        }
                        TotalCostPaidForItem += CollectionPrice;
                        if (ItemOrdersCount == 0)
                        {
                            break;
                        }

                    }

                    totalCost += TotalCostPaidForItem;
                    totalMony += TotalProfitForIItem;
                }

                TotalProfit = (totalMony - totalCost);


                return new Evaluation { TotalProfit = TotalProfit ,TotalCost=totalCost,TotalMony=totalMony };

            }
            catch (Exception ex )
            {

                throw new CanNotEvaluateExeption(ex.Message, ex);
            }
        }
    }
}
