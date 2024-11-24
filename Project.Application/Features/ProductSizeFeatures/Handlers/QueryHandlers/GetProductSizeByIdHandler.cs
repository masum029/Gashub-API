using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Domail.Abstractions;

namespace Project.Application.Features.ProductSizeFeatures.Handlers.QueryHandlers
{
    public class GetProductSizeByIdQuery : IRequest<ProductSizeDTO>
    {
        public GetProductSizeByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
    public class GetProductSizeByIdHandler : IRequestHandler<GetProductSizeByIdQuery, ProductSizeDTO>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;

        public GetProductSizeByIdHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }
       
        public async Task<ProductSizeDTO> Handle(GetProductSizeByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var productSize = await _unitOfWorkDb.productSizeQueryRepository.GetByIdAsync(request.Id);
                var newProductSize = _mapper.Map<ProductSizeDTO>(productSize);
                return newProductSize;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
