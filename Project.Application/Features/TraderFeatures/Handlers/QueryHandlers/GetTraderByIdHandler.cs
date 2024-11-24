using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Domail.Abstractions;


namespace Project.Application.Features.TraderFeatures.Handlers.QueryHandlers
{
    public class GetTraderByIdQuery : IRequest<TraderDTO>
    {
        public GetTraderByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }

    }
    public class GetTraderByIdHandler : IRequestHandler<GetTraderByIdQuery, TraderDTO>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;

        public GetTraderByIdHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }

        public async Task<TraderDTO> Handle(GetTraderByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var trader = await _unitOfWorkDb.traderQueryRepository.GetByIdAsync(request.Id);
                var newTrader = _mapper.Map<TraderDTO>(trader);
                return newTrader;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
