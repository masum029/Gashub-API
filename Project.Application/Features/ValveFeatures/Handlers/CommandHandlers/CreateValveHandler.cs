using MediatR;
using Project.Application.ApiResponse;
using Project.Domail.Abstractions;
using Project.Domail.Entities;
using System.ComponentModel.DataAnnotations;
using System.Net;


namespace Project.Application.Features.ValveFeatures.Handlers.CommandHandlers
{
    public class CreateValveCommand : IRequest<ApiResponse<string>>
    {
        [Required]
        public string? Name { get; set; }
        [Required]
        public string? Unit { get; set; }
        [Required]
        public string CreatedBy { get; set; }
    }
    public class CreateValveHandler : IRequestHandler<CreateValveCommand, ApiResponse<string>>
    {
        private readonly IUnitOfWorkDb _unitOfWorkDb;

        public CreateValveHandler(IUnitOfWorkDb unitOfWorkDb)
        {
            _unitOfWorkDb = unitOfWorkDb;
        }


        public async Task<ApiResponse<string>> Handle(CreateValveCommand request, CancellationToken cancellationToken)
        {
            var response = new ApiResponse<string>();

            try
            {
                var ExjistingValveList = await _unitOfWorkDb.valverQueryRepository.GetAllAsync();
                foreach (var valve in ExjistingValveList)
                {
                    if (valve?.Name?.Trim() == request?.Name?.Trim())
                    {
                        response.Success = false;
                        response.Data = "An error occurred while creating the Valve";
                        response.ErrorMessage = "DuplicateValveName: Valve name already exists.";
                        response.Status = HttpStatusCode.InternalServerError;

                        return response;
                    }
                }
                var newValve = new Valve
                {
                    Id = Guid.NewGuid(),
                    CreationDate = DateTime.Now.Date,
                    CreatedBy = request.CreatedBy,
                    Name = request.Name,
                    Unit = request.Unit,
                    IsActive = true,
                };

                await _unitOfWorkDb.valveCommandRepository.AddAsync(newValve);
                await _unitOfWorkDb.SaveAsync();
                response.Success = true;
                response.Data = $"Valve id = {newValve.Id} Created Successfully!";
                response.Status = HttpStatusCode.OK; // Set status code to 200 (OK)
            }
            catch (Exception ex)
            {
                response.Success = false;
                response.Data = "Server Error";
                response.ErrorMessage = ex.Message;
                response.Status = HttpStatusCode.InternalServerError; // Set status code to 500 (Internal Server Error)
            }

            return response;
        }
    }
}
