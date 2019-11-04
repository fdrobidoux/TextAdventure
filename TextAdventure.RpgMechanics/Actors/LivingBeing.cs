using System;
using System.Collections.Generic;
using SadConsole;
using SadConsole.Entities;
using TextAdventure.RpgMechanics.Calculations;

namespace TextAdventure.RpgMechanics.Actors
{
    public abstract class LivingBeing
    {
        public Health Health { get; set; }

        public bool IsAlive => Health.CurrentValue != 0;
        
        public LivingBeing()
        {
            
        }


    }
}
