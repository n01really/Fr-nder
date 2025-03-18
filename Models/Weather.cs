using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Fränder.Models
{
    class Weather
    {
        // Temperatur i grader Celsius
        public float Temp { get; set; }

        // Upplevd temperatur i grader Celsius
        public float FeelsLike { get; set; }

        // Luftfuktighet i procent
        public float Humidity { get; set; }

        // Vindhastighet i meter per sekund
        public float WindSpeed { get; set; }
    }
}
