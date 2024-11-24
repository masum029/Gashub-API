using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Domail.Abstractions;

namespace Project.Application.Features.ProdReturnFeatures.Handlers.QueryHandlers
{
    public class GetAllConfirmProdReturnQuery : IRequest<IEnumerable<ProdReturnDTO>>
    {
    }
    public class GetAllConfirmProdReturnHandler : IRequestHandler<GetAllConfirmProdReturnQuery, IEnumerable<ProdReturnDTO>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;
        public GetAllConfirmProdReturnHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }


        public async Task<IEnumerable<ProdReturnDTO>> Handle(GetAllConfirmProdReturnQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var prodReturnList = await _unitOfWorkDb.prodReturnQueryRepository.GetAllAsync();
                var ConfirmRetrurnProduct = prodReturnList.Where(rp=>rp.IsConfirmedOrder);
                var result = ConfirmRetrurnProduct.Select(x => _mapper.Map<ProdReturnDTO>(x));
                
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
