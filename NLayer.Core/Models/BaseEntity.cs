namespace NLayer.Core.Models
{
    public class BaseEntity
    {
        public int Id { get; set; }
        public DateTime CreatedDate { get; set; } = DateTime.Now.Date;
        public DateTime? UpdatedDate { get; set; }
    }
}
