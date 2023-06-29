using System.ComponentModel.DataAnnotations.Schema;

namespace DriversManagement.Models.Data.Entities
{
    [Table("Roles")]
    public class Role : BaseEntity
    {
        public string Title { get; set; } = string.Empty;
    }
}
