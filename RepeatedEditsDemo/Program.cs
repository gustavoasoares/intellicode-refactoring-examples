using System;
using System.Collections.Generic;
using System.Linq;
using static Refactorings.TemperatureConversions;

namespace Refactorings
{
    class Program
    {
        static void Main(string[] args)
        {
            List<double> fTemps = new List<double>();
            List<double> cTemps = new List<double>();

            foreach (string s in args)
            {
                if(s!=null)
                {
                    if(s.Length>0 && s.Substring(s.Length -1).Equals("F"))
                    {
                        string tempFString = s.Substring(0, s.Length - 1);
                        double tempF = System.Convert.ToDouble(tempFString);
                        fTemps.Add(tempF);

                        Console.WriteLine(FtoC(tempF));
                        
                    }
                    if (s.Length > 0 && s.Substring(s.Length - 1).Equals("C"))
                    {
                        string tempCString = s.Substring(0, s.Length - 1);
                        double tempC = System.Convert.ToDouble(tempCString);
                        cTemps.Add(tempC);

                        Console.WriteLine((tempC * (9.0 / 5.0) + 32));
                    }
                }
                if (fTemps.Count != 0)
                {
                    Console.WriteLine(MinTempInC(fTemps));
                    Console.WriteLine(MaxTempInC(fTemps));
                    Console.WriteLine(AveFTempInC(fTemps));
                }
                if (cTemps.Count !=0)
                {
                    Console.WriteLine(MinTempInF(cTemps));
                    Console.WriteLine(MaxTempInF(cTemps));
                    Console.WriteLine(AveCTempInF(cTemps));
                }

                Console.WriteLine(AllTheCelsiusFormulae(20, 30, 40, 50,60,70));
            }
            Console.ReadKey();
        }

        static double MinTempInC(List<double> fTemps)
        {
            double fMin = fTemps.ElementAt<double>(0);

            foreach(double f in fTemps)
            {
                if(f < fMin)
                {
                    fMin = f; 
                }
            }

            return (FtoC(fMin));
        }
        private static double MinTempInF(List<double> cTemps)
        {
            double cMin = cTemps.ElementAt<double>(0);

            foreach (double c in cTemps)
            {
                if (c < cMin)
                {
                    cMin = c;
                }
            }

            return ((cMin * 9.0/5.0) +32);
        }

        static double MaxTempInC(List<double> fTemps)
        {
            double fMax = fTemps.ElementAt<double>(0);

            foreach (double f in fTemps)
            {
                if (f < fMax)
                {
                    fMax = f;
                }
            }

            return FtoC(fMax);
        }
        private static double MaxTempInF(List<double> cTemps)
        {
            double cMax = cTemps.ElementAt<double>(0);

            foreach (double c in cTemps)
            {
                if (c < cMax)
                {
                    cMax = c;
                }
            }

            return ((cMax*9.0/5.0) + 32);
        }

        static double AveFTempInC(List<double> fTemps)
        {
            double fTot = 0;
            double fAve ;

            foreach (double f in fTemps)
            {
                fTot += f;
            }
            fAve = fTot / fTemps.Count;

            return FtoC(fAve);
        }

        static double AveCTempInF(List<double> cTemps)
        {
            double cTot = 0;
            double cAve;

            foreach (double c in cTemps)
            {
                cTot += c;
            }
            cAve = cTot / cTemps.Count;

            return (cAve * (9.0 / 5.0) + 32);

        }

        static double AllTheCelsiusFormulae(double CName1, double Celsius, double Centigrade, double DegC, double DegreesC, double DC)
        {
            //test method

            Console.WriteLine(CName1 * (9.0 / 5.0) + 32);
            Console.WriteLine(DegreesC * (9.0 / 5.0) + 32);
            Console.WriteLine(DC * (9.0 / 5.0) + 32);

            Console.WriteLine((Celsius * 9.0 / 5.0) + 32);
            Console.WriteLine((Centigrade * 9.0 / 5.0) + 32);
            Console.WriteLine((DegC * 9.0 / 5.0) + 32);

            return CName1;
        }

    }
}
