using Project.Domail.Entities.Base;

namespace Project.Domail.Entities
{
    public class Valve : BaseEntity
    {
        public string ? Name { get; set; }
        public string ? Unit { get; set; }
        public bool IsActive { get; set; }

    }
}
