using System.ComponentModel.DataAnnotations.Schema;

namespace DriversManagement.Models.Data.Entities
{
    [Table("Cars")]
    public class Car : BaseEntity
    {
        public string? Model { get; set; }
    }
}
