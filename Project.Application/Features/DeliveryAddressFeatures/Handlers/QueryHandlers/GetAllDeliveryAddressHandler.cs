using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Application.Interfaces;
using Project.Domail.Abstractions;

namespace Project.Application.Features.DeliveryAddressFeatures.Handlers.QueryHandlers
{
    public class GetAllDeliveryAddressQuery : IRequest<IEnumerable<DeliveryAddressDTO>>
    {
    }
    public class GetAllDeliveryAddressHandler : IRequestHandler<GetAllDeliveryAddressQuery, IEnumerable<DeliveryAddressDTO>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;
        public GetAllDeliveryAddressHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }


        public async Task<IEnumerable<DeliveryAddressDTO>> Handle(GetAllDeliveryAddressQuery request, CancellationToken cancellationToken)
        {
            try
            {
                
                var deliveryAddressList = await _unitOfWorkDb.deliveryAddressQueryRepository.GetAllAsync();
                var deliveryAddressDto = deliveryAddressList.Select(item => _mapper.Map<DeliveryAddressDTO>(item));
                return deliveryAddressDto;
               
            }
            catch (Exception)
            {

                throw;
            }

            
        }
    }
}
