using System.ComponentModel.DataAnnotations.Schema;

namespace DriversManagement.Models.Data.Entities
{
    [Table("Deliveries")]
    public class Delivery : BaseEntity
    {
        public int DriverId { get; set; }

        public int CarId { get; set; }

        public int ShipmentId { get; set; }

        public string? DestinationAddress { get; set; }

        public int Price { get; set; }


        #region relations

        public User? Driver { get; set; }

        public Car? Car { get; set; }

        public Shipment? Shipment { get; set; }

        #endregion
    }
}
