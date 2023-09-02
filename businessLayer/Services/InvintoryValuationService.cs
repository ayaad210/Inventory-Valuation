using businessLayer.Dtos.InventoryEvaluation;
using businessLayer.Enums;
using businessLayer.IServices;
using DataAccessLayer.UnitOfWork_;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace businessLayer.Services
{
    public class InvintoryValuationService :IInvintoryValuationService
    {
        private  IInvintoryValuationStrategy invintoryValuationStrategy;
        private IUnitOfWork _unitOfWork;
        private readonly ILogger<InvintoryValuationService> logger;

        public InvintoryValuationService(IUnitOfWork unitOfWork , ILogger<InvintoryValuationService> logger)
        {
            _unitOfWork=unitOfWork;
            this.logger = logger;
       //     invintoryValuationStrategy = new LiFOValuation(_unitOfWork);

        }
        public Task  SetStrategy(ValuationMethods valuationMethods )
        {
            try
            {
                switch (valuationMethods)
                {
                    case ValuationMethods.FIFO:
                        invintoryValuationStrategy = new FIFOValuation(_unitOfWork);
                        break;
                    case ValuationMethods.LIFO:
                        invintoryValuationStrategy = new LiFOValuation(_unitOfWork);
                        break;
                    default:
                        break;   
                }
                return Task.CompletedTask;
            }
            catch (Exception ex)
            {
                logger.LogError(ex, "Can Not Evaluate Inventory {Message}", ex.Message);

                throw;
            }
        }


        public async Task<Evaluation> EvaluateInventory()
        {
            if ( this.invintoryValuationStrategy !=null)
            {
                return await this.invintoryValuationStrategy.EvaluateInventory();

            }
            return  new Evaluation { TotalProfit=0};

        }
    }
}
