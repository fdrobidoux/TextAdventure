using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

namespace TextAdventure.RpgMechanics.Calculations
{
    public abstract class Stat<T> where T : struct
    {
        protected List<Modifier<T>> modifiers;

        public T Value { get; private set; }

        public T ProcessModifiers()
        {
            T currentValue = Value;

            foreach (Modifier<T> mod in modifiers)
            {
                currentValue = mod.Modify(currentValue);
            }

            return currentValue;
        }
    }
}
