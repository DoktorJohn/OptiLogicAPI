using System.ComponentModel.DataAnnotations.Schema;
using System.ComponentModel.DataAnnotations;

namespace OptiLogic_API.Model
{
    [Table("Parking_Spot")]
    public class ParkingSpot
    {
        [Key]
        [Column("parking_spot_id")]
        public int ParkingSpotID { get; set; }

        [Column("x_axis")]
        public float XAxis { get; set; }

        [Column("y_axis")]
        public float YAxis { get; set; }

        [Column("width")]
        public float Width { get; set; }

        [Column("height")]
        public float Height { get; set; }

        [Column("occupied")]
        public bool Occupied { get; set; }

        [Column("parking_lot_id")]
        public int ParkingLotId { get; set; }

        [Column("car_id")]
        public int? CarId { get; set; }
    }
}
