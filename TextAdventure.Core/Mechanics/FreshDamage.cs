using System;
using System.Collections.Generic;
using System.Text;
using TextAdventure.Core.UI;

namespace TextAdventure.Core.Mechanics
{
    public class FreshDamage
    {
        public HealthProgressBar parent;

        public FreshDamage(HealthProgressBar parent)
        {
            this.parent = parent ?? throw new ArgumentNullException(nameof(parent));
        }

        public float Value { get; set; }


    }
}
