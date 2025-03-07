namespace OptiLogic_API.HelperModel
{
    public class Prediction
    {
        public int Car_Id { get; set; }
        public float X { get; set; }
        public float Y { get; set; }
        public float Width { get; set; }
        public float Height { get; set; }
        public float Confidence { get; set; }
        public string Class { get; set; }
        public int Parking_Lot_Id { get; set; }

    }

    public class PredictionRequest
    {
        public List<Prediction> Predictions { get; set; }
    }
}
