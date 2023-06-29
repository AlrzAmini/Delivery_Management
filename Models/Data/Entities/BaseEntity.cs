using System.ComponentModel.DataAnnotations;

namespace DriversManagement.Models.Data.Entities
{
    public abstract class BaseEntity
    {
        [Key]
        public int Id { get; set; }

        public bool IsDeleted { get; set; }

        public DateTime CreateDate { get; set; } = DateTime.Now;
    }
}
