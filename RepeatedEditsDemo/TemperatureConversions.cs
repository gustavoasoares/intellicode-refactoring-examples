namespace Refactorings
{
    public static class TemperatureConversions
    {
        public static double FtoC(double temperatureInF)
        {
            //T(°C) = (T(°F) - 32) × 5 / 9
            double temperatureInC = (temperatureInF - 32) * (5.0 / 9.0);
            return temperatureInC;
        }

        public static double CtoF(double temperatureInC)
        {
            //T(°F) = (T(°C) × 9 / 5) + 32
            double temperatureInF = (temperatureInC * (9.0 / 5.0)) + 32;
            return temperatureInF;
        }

        /// <summary>
        /// Converts temperatures from one unit of measure to another
        /// </summary>
        /// <param name="Conversion">the input and output units desired</param>
        /// <param name="temperature">temperature to be converted</param>
        /// <returns>temperature in the specified output unit</returns>
       public static double Convert(Conversion conversion,  double temperature)
        {
            double outTemperature = temperature;

            if (conversion == Conversion.FtoC)
            {
                //F to C: T(°C) = (T(°F) - 32) × 5 / 9
                outTemperature = (temperature - 32) * (5.0 / 9.0);
            }
            
            if(conversion == Conversion.CtoF)
            {
                //C to F: T(°F) = (T(°C) × 9 / 5) + 32
                outTemperature = (temperature * (9.0 / 5.0)) + 32;

            }

            return outTemperature;
        }
        /// <summary>
        /// Temperature units for conversion
        /// </summary>
        public enum Conversion{ FtoC, CtoF }

    }
}
