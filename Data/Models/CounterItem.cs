using System.ComponentModel.DataAnnotations;

namespace GrinHome.Data.Models
{
    public class CounterItem
    {
        public ushort ID { get; set; }
        [Required]
        [StringLength(20)]
        public string Name { get; set; } = null!;

        [Required]
        public DateTime FirstDay { get; set; }

        public virtual IList<CounterValue> Values { get; set; } = new List<CounterValue>();


        internal int ValueToAdd { get; set; }

        public async Task AddValue(DateTime now, int valueToAdd, ApplicationDbContext? db)
        {
            if (db == null)
                return;

            var curValue = db.CounterValues.FirstOrDefault(x => x.Date == now.Date);
            if(curValue==null)
            {
                await db.CounterValues.AddAsync(new CounterValue() {CounterItemID = ID, Date = now.Date, Value = valueToAdd});
            }
            else
            {
                curValue.Value += valueToAdd;
            }
            await db.SaveChangesAsync();
        }

        public int GetSumValues()
        {
            if (Values == null)
                return 0;
            return Values.Sum(x => x.Value);
        }
    }
}
