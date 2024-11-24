using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Domail.Abstractions;

namespace Project.Application.Features.ProductDiscuntFeatures.Handlers.QueryHandlers
{
    public class GetAllProductDiscuntQuery : IRequest<IEnumerable<ProductDiscuntDTO>>
    {
    }
    public class GetAllProductDiscuntHandler : IRequestHandler<GetAllProductDiscuntQuery, IEnumerable<ProductDiscuntDTO>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;
        public GetAllProductDiscuntHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }


        public async Task<IEnumerable<ProductDiscuntDTO>> Handle(GetAllProductDiscuntQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var productDiscuntList = await _unitOfWorkDb.productDiscuntQueryRepository.GetAllAsync();
                var result = productDiscuntList.Select(x => _mapper.Map<ProductDiscuntDTO>(x));

                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
