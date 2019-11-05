using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventure.RpgMechanics.Calculations
{
    public class Experience : ProgressStat<int>
    {
        

        public Experience() : base(100, 0, true)
        {
            this.OverflowingValue += Experience_OverflowingValue;
        }

        private void Experience_OverflowingValue(object sender, OverflowingValueEventArgs<int> e)
        {
            // TODO: Level-up stuff.
        }
    }
}
