using System;
using System.Collections.Generic;

namespace Steakanizer
{
    internal class Filet
    {
        static TimeSpan fiftySeconds = TimeSpan.FromSeconds(15);
        static TimeSpan thirtySeconds = TimeSpan.FromSeconds(30);
        static TimeSpan oneMinute = TimeSpan.FromMinutes(1);
        static TimeSpan twoMinutes = TimeSpan.FromMinutes(2);
        static TimeSpan threeMinutes = TimeSpan.FromMinutes(3);
        static TimeSpan fourMinutes = TimeSpan.FromMinutes(4);
        static TimeSpan fiveMinutes = TimeSpan.FromMinutes(5);
        static TimeSpan sixMinute = TimeSpan.FromMinutes(6);

        public IEnumerable<CookingDegree> CookingTimeByThickness(double thickeness)
        {
            var cookingDegrees = new List<CookingDegree>();

            switch (thickeness)
            {
                case 1.0:
                    cookingDegrees.Add(new CookingDegree(CookingDegreeTypes.Bleu, fiftySeconds));
                    cookingDegrees.Add(new CookingDegree(CookingDegreeTypes.English, thirtySeconds));
                    cookingDegrees.Add(new CookingDegree(CookingDegreeTypes.Medium, oneMinute));
                    cookingDegrees.Add(new CookingDegree(CookingDegreeTypes.MediumRare, twoMinutes));
                    cookingDegrees.Add(new CookingDegree(CookingDegreeTypes.MediumWell, threeMinutes));
                    cookingDegrees.Add(new CookingDegree(CookingDegreeTypes.WellDone, fourMinutes));
                    break;
                case 2.0:
                    cookingDegrees.Add(new CookingDegree(CookingDegreeTypes.Bleu, thirtySeconds));
                    cookingDegrees.Add(new CookingDegree(CookingDegreeTypes.English, oneMinute));
                    cookingDegrees.Add(new CookingDegree(CookingDegreeTypes.Medium, twoMinutes));
                    cookingDegrees.Add(new CookingDegree(CookingDegreeTypes.MediumRare, threeMinutes));
                    cookingDegrees.Add(new CookingDegree(CookingDegreeTypes.MediumWell, fourMinutes));
                    cookingDegrees.Add(new CookingDegree(CookingDegreeTypes.WellDone, fiveMinutes));
                    break;
                case 3.0:
                    cookingDegrees.Add(new CookingDegree(CookingDegreeTypes.Bleu, thirtySeconds));
                    cookingDegrees.Add(new CookingDegree(CookingDegreeTypes.English, oneMinute));
                    cookingDegrees.Add(new CookingDegree(CookingDegreeTypes.Medium, twoMinutes));
                    cookingDegrees.Add(new CookingDegree(CookingDegreeTypes.MediumRare, threeMinutes));
                    cookingDegrees.Add(new CookingDegree(CookingDegreeTypes.MediumWell, fourMinutes));
                    cookingDegrees.Add(new CookingDegree(CookingDegreeTypes.WellDone, fiveMinutes));
                    break;
                case 4.0:
                    cookingDegrees.Add(new CookingDegree(CookingDegreeTypes.Bleu, thirtySeconds));
                    cookingDegrees.Add(new CookingDegree(CookingDegreeTypes.English, oneMinute));
                    cookingDegrees.Add(new CookingDegree(CookingDegreeTypes.Medium, twoMinutes));
                    cookingDegrees.Add(new CookingDegree(CookingDegreeTypes.MediumRare, threeMinutes));
                    cookingDegrees.Add(new CookingDegree(CookingDegreeTypes.MediumWell, fourMinutes));
                    cookingDegrees.Add(new CookingDegree(CookingDegreeTypes.WellDone, fiveMinutes));
                    break;
                case 5.0:
                    cookingDegrees.Add(new CookingDegree(CookingDegreeTypes.Bleu, thirtySeconds));
                    cookingDegrees.Add(new CookingDegree(CookingDegreeTypes.English, oneMinute));
                    cookingDegrees.Add(new CookingDegree(CookingDegreeTypes.Medium, twoMinutes));
                    cookingDegrees.Add(new CookingDegree(CookingDegreeTypes.MediumRare, threeMinutes));
                    cookingDegrees.Add(new CookingDegree(CookingDegreeTypes.MediumWell, fourMinutes));
                    cookingDegrees.Add(new CookingDegree(CookingDegreeTypes.WellDone, fiveMinutes));
                    break;
                default:
                    break;
            }

            return cookingDegrees;
        }
    }
}
