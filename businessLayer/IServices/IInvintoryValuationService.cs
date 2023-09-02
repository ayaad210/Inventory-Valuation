using businessLayer.Dtos.InventoryEvaluation;
using businessLayer.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace businessLayer.IServices
{
    public interface IInvintoryValuationService
    {
        public Task SetStrategy(ValuationMethods valuationMethods);

        public Task<Evaluation> EvaluateInventory();


    }
}
