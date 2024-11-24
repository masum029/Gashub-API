using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Application.Features.ProdReturnFeatures.Handlers.QueryHandlers;
using Project.Domail.Abstractions;

namespace Project.Application.Features.ProductDiscuntFeatures.Handlers.QueryHandlers
{
    public class GetProductDiscuntByIdQuery : IRequest<ProductDiscuntDTO>
    {
        public GetProductDiscuntByIdQuery(Guid id)
        {
            Id = id;
        }

        public Guid Id { get; private set; }
    }
    public class GetProductDiscuntByIdHandler : IRequestHandler<GetProductDiscuntByIdQuery, ProductDiscuntDTO>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;

        public GetProductDiscuntByIdHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }

        public async Task<ProductDiscuntDTO> Handle(GetProductDiscuntByIdQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var productDiscount = await _unitOfWorkDb.productDiscuntQueryRepository.GetByIdAsync(request.Id);
                var newproductDiscount = _mapper.Map<ProductDiscuntDTO>(productDiscount);
                return newproductDiscount;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
