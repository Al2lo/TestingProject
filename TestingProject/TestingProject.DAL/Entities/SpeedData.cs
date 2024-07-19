using Microsoft.AspNetCore.Mvc;
using System.ComponentModel.DataAnnotations;
using System.Diagnostics.CodeAnalysis;
using System.Globalization;

namespace TestingProject.DAL.Entities
{
    public class SpeedData
    {

        public string TimestampString
        {
            set
            {
                DateTime = DateTime.ParseExact(value, "dd.MM.yyyy HH:mm:ss", CultureInfo.InvariantCulture);
            }
        }
        public DateTime DateTime { get; private set; }
        public string CarNumber { get; set; }
        public double Speed { get; set; }
    }
}
