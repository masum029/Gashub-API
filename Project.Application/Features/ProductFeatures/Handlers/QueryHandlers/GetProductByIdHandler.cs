using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Domail.Abstractions;


namespace Project.Application.Features.ProductFeatures.Handlers.QueryHandlers
{
    public class GetProductByIdQuery : IRequest<ProductDTO>
    {
        public GetProductByIdQuery(Guid id)
        {
            this.id = id;
        }

        public Guid id { get; private set; }
    }
    public class GetProductByIdHandler : IRequestHandler<GetProductByIdQuery, ProductDTO>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;

        public GetProductByIdHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }
  
        public async Task<ProductDTO> Handle(GetProductByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var product = await _unitOfWorkDb.productQueryRepository.GetByIdAsync(request.id);
                var newProduct = _mapper.Map<ProductDTO>(product);
                return newProduct;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
