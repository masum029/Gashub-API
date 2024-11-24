using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Domail.Abstractions;

namespace Project.Application.Features.OrderFeatures.Handlers.QueryHandlers
{
    public class GetOrderByIdQuery : IRequest<OrderDTO>
    {
        public GetOrderByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
    public class GetOrderByIdHandler : IRequestHandler<GetOrderByIdQuery, OrderDTO>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;

        public GetOrderByIdHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }
        public async Task<OrderDTO> Handle(GetOrderByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var order = await _unitOfWorkDb.orderQueryRepository.GetByIdAsync(request.Id);
                var neworder = _mapper.Map<OrderDTO>(order);
                return neworder;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
