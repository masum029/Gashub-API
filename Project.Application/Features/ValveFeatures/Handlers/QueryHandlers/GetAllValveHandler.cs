using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Domail.Abstractions;


namespace Project.Application.Features.ValveFeatures.Handlers.QueryHandlers
{
    public class GetAllValveQuery : IRequest<IEnumerable<ValveDTO>>
    {
    }
    public class GetAllValveHandler : IRequestHandler<GetAllValveQuery, IEnumerable<ValveDTO>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;
        public GetAllValveHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }
 
        public async Task<IEnumerable<ValveDTO>> Handle(GetAllValveQuery request, CancellationToken cancellationToken)
        {
            try
            {
                var valveList = await _unitOfWorkDb.valverQueryRepository.GetAllAsync();
                var result = valveList.Select(x => _mapper.Map<ValveDTO>(x));
                return result;
            }
            catch (Exception)
            {

                throw;
            }
        }
    }
}
