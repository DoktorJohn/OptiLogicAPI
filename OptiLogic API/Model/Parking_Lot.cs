using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OptiLogic_API.Model
{
    [Table("Parking_Lot")]
    public class ParkingLot
    {
        [Key]
        [Column("pl_id")]
        public int Id { get; set; }

        [Column("pl_name")]
        [StringLength(255)]
        public string Name { get; set; }

        public ICollection<Car> Cars { get; set; } = new List<Car>();
        public ICollection<ParkingSpot> ParkingSpots { get; set; } = new List<ParkingSpot>();
    }
}
