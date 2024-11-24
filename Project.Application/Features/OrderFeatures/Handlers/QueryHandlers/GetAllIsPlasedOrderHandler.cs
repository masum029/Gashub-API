using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Domail.Abstractions;

namespace Project.Application.Features.OrderFeatures.Handlers.QueryHandlers
{
    public class GetAllIsPlasedOrderQuery : IRequest<IEnumerable<OrderDTO>>
    {
        
    }
    public class GetAllIsPlasedOrderHandler : IRequestHandler<GetAllIsPlasedOrderQuery, IEnumerable<OrderDTO>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;
        public GetAllIsPlasedOrderHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }

        public async Task<IEnumerable<OrderDTO>> Handle(GetAllIsPlasedOrderQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var orders = await _unitOfWorkDb.orderQueryRepository.GetAllAsync();

                // Use Where to filter placed orders
                var isPlacedOrders = orders.Where(odr => odr.IsPlaced && !odr.IsConfirmed && !odr.IsReadyToDispatch && !odr.IsDispatched && !odr.IsDelivered);

                // Map the filtered orders to OrderDTO
                var ordersDto = isPlacedOrders.Select(item => _mapper.Map<OrderDTO>(item));
                return ordersDto;
            }
            catch (Exception)
            {

                throw;
            }
        }

    }
}
