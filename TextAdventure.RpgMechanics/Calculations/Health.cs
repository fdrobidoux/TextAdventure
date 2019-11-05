using System;
using System.Collections.Generic;
using System.Text;
using TextAdventure.RpgMechanics.Actors;

namespace TextAdventure.RpgMechanics.Calculations
{
    public class Health : ProgressStat<int>
    {
        
        public Health(int maxValue) : this(maxValue, maxValue)
        {

        }

        public Health(int maxValue, int startValue) : base(maxValue, startValue, willTrackOverflow: true)
        {

        }
    }
}
