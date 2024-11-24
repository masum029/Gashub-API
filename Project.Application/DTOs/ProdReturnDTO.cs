using Project.Domail.Entities;

namespace Project.Application.DTOs
{
    public class ProdReturnDTO
    {
        public Guid Id { get; set; }
        public Guid ProductId { get; set; }
        public string Name { get; set; }
        public Guid ProdSizeId { get; set; }
        public Guid ProdValveId { get; set; }
        public string CreatedBy { get; set; }
        public string UpdatedBy { get; set; }
        public bool IsConfirmedOrder { get; set; }
        public virtual Product Product { get; set; }
    }
}
