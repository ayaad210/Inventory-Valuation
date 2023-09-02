using businessLayer.Dtos.Item;
using businessLayer.Dtos.Request;
using DataAccessLayer.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace businessLayer.IServices
{
    public interface IRequestService
    {
        public Task<RequestCreateDto> CreateRequest(RequestCreateDto ItemCDto);

    }
}
