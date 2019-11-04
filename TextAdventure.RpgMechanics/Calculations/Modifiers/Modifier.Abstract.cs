using System;
using System.Collections.Generic;
using System.Linq.Expressions;
using System.Text;

namespace TextAdventure.RpgMechanics.Calculations
{
    public abstract class Modifier<T> where T : struct
    {
        public BlockExpression Block { get; protected set; }

        public abstract T Modify(T currentValue);
    }
}
