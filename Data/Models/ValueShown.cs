using System.ComponentModel.DataAnnotations;

namespace GrinHome.Data.Models
{
    public class ValueShown
    {
        public ushort ID { get; set; }
        [Required]
        [StringLength(20)]
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        public string Page { get; set; }

        public ushort Order { get; set; }

        public ValueDefinition ValueDefinition { get; set; }
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.

        /// <summary>
        /// Return NaN if no values
        /// </summary>
        /// <returns></returns>
        public float GetValue()
        {
            if (ValueDefinition == null || ValueDefinition.SensorValues == null || !ValueDefinition.SensorValues.Any())
                return float.NaN;
            return ValueDefinition.SensorValues.First().Value;
        }

        public void SetValue(float value)
        {
            if (ValueDefinition == null || ValueDefinition.SensorValues == null || !ValueDefinition.SensorValues.Any())
                return;
            ValueDefinition.SensorValues.First().Value = value;
        }
    }
}
