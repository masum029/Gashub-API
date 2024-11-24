using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Domail.Abstractions;

namespace Project.Application.Features.RetailerFeatures.Handlers.QueryHandlers
{
    public class GetAllRetailerQuery : IRequest<IEnumerable<RetailerDTO>>
    {
    }
    public class GetAllRetailerHandler : IRequestHandler<GetAllRetailerQuery, IEnumerable<RetailerDTO>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;
        public GetAllRetailerHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }
        public async Task<IEnumerable<RetailerDTO>> Handle(GetAllRetailerQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var retailerList = await _unitOfWorkDb.retailerQueryRepository.GetAllAsync();
                var result = retailerList.Select(x => _mapper.Map<RetailerDTO>(x));
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
