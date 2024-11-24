using Project.Domail.Abstractions.CommandRepositories.Base;
using Project.Domail.Entities;

namespace Project.Domail.Abstractions.CommandRepositories
{
    public interface IOrderCommandRepository : ICommandRepository<Order>
    {
        // Add specific command methods here if needed
        Task<bool> ConfirmOrder( Guid UserID , Dictionary<string,int> itemQuantities);
        Task<bool> UpdateFinalOrder( Order item);
    }
}
