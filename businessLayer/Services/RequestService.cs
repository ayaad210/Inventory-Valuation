using AutoMapper;
using businessLayer.Dtos.Request;
using businessLayer.exeptions;
using DataAccessLayer.IRepository;
using DataAccessLayer.Models;
using DataAccessLayer.UnitOfWork_;
using Microsoft.Extensions.Logging;

namespace businessLayer.IServices
{
    public class RequestService : IRequestService
    {
        private readonly IItemRepo  itemRepo;

        private readonly IRequestRepo RequestRepository;
        private readonly IUnitOfWork unitOfWork;
        private readonly IMapper _mapper;
        private readonly ILogger<RequestService> logger;

        public RequestService(IUnitOfWork unitOfWork, IMapper mapper, ILogger<RequestService> logger)
        {
            this.RequestRepository = unitOfWork.RequestRepository();
            itemRepo = unitOfWork.ItemRepository();
            this.unitOfWork = unitOfWork;
            this._mapper = mapper;
            this.logger = logger;
        }





        public async Task<RequestCreateDto> CreateRequest(RequestCreateDto RequestCDto)
        {
            try
            {
                Request Request = _mapper.Map<Request>(RequestCDto);

                Request res = await this.RequestRepository.Insert(Request);
                RequestCreateDto RequestCreateDto = _mapper.Map<RequestCreateDto>(res);
             
                    Item item =  await    itemRepo.GetByID(RequestCDto.ItemId);
                    item.TotalCount = item.TotalCount + RequestCDto.count;
                     await  itemRepo.Update(item);
                      bool success=    await unitOfWork.Save();

                    if (!success)
                    {


                        throw new CanNotCreateExeption("Database Error","Request");

                    }


                
                return RequestCreateDto;


            }
            catch (Exception ex)
            {

                await unitOfWork.RollBackChangesAsync();

                logger.LogError(ex, "Can Not Create Request {Message}", ex.Message);

                throw;

            }
        }


    }
}
