using Serilog;

namespace GrinHome.Data.Models
{
    public class SensorValue : IModelDb<SensorValue>
    {
        public long ID { get; set; }
        public DateTime Date { get; set; }
        public float Value { get; set; }

        public ushort ValueDefinitionID { get; set; }


#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public virtual ValueDefinition ValueDefinition { get; set; }

        public async Task<bool> AddAsync(ApplicationDbContext? db)
        {
            try
            {
                if (db == null)
                    return false;
                await db.SensorValues.AddAsync(this);
                await db.SaveChangesAsync();
                return true;
            }
            catch (Exception ex)
            {
                Log.Error("SensorValue Add error", ex);
            }
            return false;
        }

        public Task<bool> DeleteAsync(ApplicationDbContext? db)
        {
            throw new NotImplementedException();
        }

        public Task<bool> UpdateAsync(ApplicationDbContext? db)
        {
            throw new NotImplementedException();
        }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
    }
}