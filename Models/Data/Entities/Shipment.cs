using System.ComponentModel.DataAnnotations.Schema;

namespace DriversManagement.Models.Data.Entities
{
    [Table("Shipments")]
    public class Shipment : BaseEntity
    {
        public string Title { get; set; }

        public int Weight { get; set; }

        public int Price { get; set; }
    }
}
