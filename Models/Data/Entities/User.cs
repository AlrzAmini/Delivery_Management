using System.ComponentModel.DataAnnotations.Schema;

namespace DriversManagement.Models.Data.Entities
{
    [Table("Users")]
    public class User : BaseEntity
    {
        public int RoleId { get; set; }

        public string? Name { get; set; }

        public string Mobile { get; set; } = string.Empty;

        public string? Password { get; set; }


        #region relations

        public Role? Role { get; set; }

        #endregion
    }
}
