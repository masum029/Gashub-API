using Project.Domail.Abstractions.CommandRepositories.Base;
using Project.Domail.Entities;

namespace Project.Domail.Abstractions.CommandRepositories
{
    public interface IProductCommandRepository : ICommandRepository<Product>
    {
        // Add specific command methods here if needed
    }
}
