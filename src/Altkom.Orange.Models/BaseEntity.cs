namespace Altkom.Orange.Models
{
    public abstract class BaseEntity : Base
    {
        public int Id { get; set; }
        public bool IsRemoved { get; set; }
    }

}
