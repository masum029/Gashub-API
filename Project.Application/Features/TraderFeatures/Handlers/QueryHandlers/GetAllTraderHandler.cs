using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Domail.Abstractions;


namespace Project.Application.Features.TraderFeatures.Handlers.QueryHandlers
{
    public class GetAllTraderQuery : IRequest<IEnumerable<TraderDTO>>
    {
    }
    public class GetAllTraderHandler : IRequestHandler<GetAllTraderQuery, IEnumerable<TraderDTO>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;
        public GetAllTraderHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }

        public async Task<IEnumerable<TraderDTO>> Handle(GetAllTraderQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var TraderList = await _unitOfWorkDb.traderQueryRepository.GetAllAsync();
                var result = TraderList.Select(x => _mapper.Map<TraderDTO>(x));
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
