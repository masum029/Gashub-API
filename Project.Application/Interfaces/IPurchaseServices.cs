using Project.Application.DTOs;


namespace Project.Application.Interfaces
{
    public interface IPurchaseServices
    {
        public Task<bool> PurchaseProduct(PurchaseItemDTOs entitys);
    }
}
