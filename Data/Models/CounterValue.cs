namespace GrinHome.Data.Models
{
    public class CounterValue
    {
        public long ID { get; set; }

        public DateTime Date { get; set; }

        public int Value { get; set; }

        public ushort CounterItemID { get; set; }
    }
}
