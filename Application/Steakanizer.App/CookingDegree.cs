using System;

namespace Steakanizer
{
    internal struct CookingDegree
    {
        public CookingDegree(CookingDegreeTypes type, TimeSpan time)
        {
            TimePerSide = time;
            Type = type;
        }
        public TimeSpan TimePerSide { get; private set; }
        public CookingDegreeTypes Type { get; private set; }
    }
}
