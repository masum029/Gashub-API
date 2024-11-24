using Project.Domail.Entities;

namespace Project.Application.DTOs
{
    public class OrderDTO
    {
        public Guid Id { get; set; }
        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public bool IsHold { get; set; }
        public bool IsCancel { get; set; }
        public Guid ReturnProductId { get; set; }
        public string CreatedBy { get; set; }
        public DateTime CreationDate { get; set; }
        public string UpdatedBy { get; set; }
        public DateTime? UpdateDate { get; set; }
        public bool IsPlaced { get; set; }
        public bool IsConfirmed { get; set; }
        public string TransactionNumber { get; set; } 
        public string Comments { get; set; }
        public bool IsDispatched { get; set; }
        public bool IsReadyToDispatch { get; set; }
        public bool IsDelivered { get; set; }
        public virtual Product Product { get; set; }
    }
}
