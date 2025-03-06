using System.ComponentModel.DataAnnotations;
using System.ComponentModel.DataAnnotations.Schema;

namespace OptiLogic_API.Model
{
    [Table("Car")]
    public class Car
    {
        [Key]
        [Column("car_id")]
        [DatabaseGenerated(DatabaseGeneratedOption.None)]
        public int CarId { get; set; }

        [Column("x_axis")]
        public float XAxis { get; set; }

        [Column("y_axis")]
        public float YAxis { get; set; }

        [Column("width")]
        public float Width { get; set; }

        [Column("height")]
        public float Height { get; set; }

        [Column("confidence")]
        public float Confidence { get; set; } 

        [Column("parking_lot_id")]
        public int ParkingLotId { get; set; } 

    }
}
