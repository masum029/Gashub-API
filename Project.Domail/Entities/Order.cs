using Project.Domail.Entities.Base;

namespace Project.Domail.Entities
{
    public class Order : BaseEntity
    {

        public Guid UserId { get; set; }
        public Guid ProductId { get; set; }
        public Guid ReturnProductId { get; set; }
        public string TransactionNumber { get; set; } 
        public bool IsHold { get; set; }
        public bool IsCancel { get; set; }
        public bool IsPlaced { get; set; }
        public bool IsConfirmed { get; set; }
        public string  Comments { get; set; }
        public bool IsDispatched { get; set; }
        public bool IsReadyToDispatch { get; set; }
        public bool IsDelivered { get; set; }

        // Navigation properties
        public virtual Product ? Product { get; set; }

    }
}
