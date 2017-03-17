namespace Model
{
    public class OrderRecord
    {
        public int Id { get; set; }
        public int OrderKods { get; set; }
        public int PartId { get; set; }
        public int WorkId { get; set; }
        public int DeviceId { get; set; }
        public string OrderType { get; set; }
    }
}