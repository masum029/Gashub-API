using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Domail.Abstractions;


namespace Project.Application.Features.StockFeatures.Handlers.QueryHandlers
{
    public class GetAllStockQuery : IRequest<IEnumerable<StockDTO>>
    {
    }
    public class GetAllStockHandler : IRequestHandler<GetAllStockQuery, IEnumerable<StockDTO>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;
        public GetAllStockHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }

        public async Task<IEnumerable<StockDTO>> Handle(GetAllStockQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var stockList = await _unitOfWorkDb.stockQueryRepository.GetAllAsync();
                var stockResp = stockList.Select(x => _mapper.Map<StockDTO>(x));
                return stockResp;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
