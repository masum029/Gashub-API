using AutoMapper;
using MediatR;
using Project.Application.DTOs;
using Project.Domail.Abstractions;

namespace Project.Application.Features.CompanyFeatures.Handlers.QueryHandlers
{
    public class GetAllCompanyQuery : IRequest<IEnumerable<CompanyDTO>>
    {
    }
    public class GetAllCompanyHandler : IRequestHandler<GetAllCompanyQuery, IEnumerable<CompanyDTO>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;
        private readonly IMapper _mapper;
        public GetAllCompanyHandler(IUnitOfWorkDb unitOfWorkDb, IMapper mapper)
        {
            _unitOfWorkDb = unitOfWorkDb;
            _mapper = mapper;
        }
        public async Task<IEnumerable<CompanyDTO>> Handle(GetAllCompanyQuery request, CancellationToken cancellationToken)
        {
            try
            {
                // Get all companies
                var companyList = await _unitOfWorkDb.companyrQueryRepository.GetAllAsync();

                // Map companies to DTOs
                var companyDtos = companyList.Select(item => _mapper.Map<CompanyDTO>(item));

                return companyDtos;
            }
            catch (Exception ex)
            {
                // Log the exception or handle it accordingly
                throw new Exception("An error occurred while retrieving companies", ex);
            }
        }

    }
}
