using System.ComponentModel.DataAnnotations;

namespace GrinHome.Data.Models
{
    public class ValueShown
    {
        public ushort ID { get; set; }
        [Required]
        [StringLength(20)]
        public string Page { get; set; } = null!;

        public ushort Order { get; set; }

        public ValueDefinition ValueDefinition { get; set; } = null!;

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
