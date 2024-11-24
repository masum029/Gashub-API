using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Domail.Abstractions;


namespace Project.Application.Features.StockFeatures.Handlers.QueryHandlers
{
    public class GetStockByIdQuery : IRequest<StockDTO>
    {
        public GetStockByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }

    }
    public class GetStockByIdHandler : IRequestHandler<GetStockByIdQuery, StockDTO>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;

        public GetStockByIdHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }
        public async Task<StockDTO> Handle(GetStockByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var stock = await _unitOfWorkDb.stockQueryRepository.GetByIdAsync(request.Id);
                var newStock = _mapper.Map<StockDTO>(stock);
                return newStock;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
