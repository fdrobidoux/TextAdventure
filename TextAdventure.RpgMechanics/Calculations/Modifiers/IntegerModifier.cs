using System;
using System.Linq.Expressions;

namespace TextAdventure.RpgMechanics.Calculations
{
    public class IntegerModifier : Modifier<int>
    {
        public int Value { get; protected set; }

        public IntegerModifier(int value)
        {
            ParameterExpression paramValue = Expression.Parameter(typeof(int), "value");

            // TODO: Modifier

            Value = value;
        }

        public override int Modify(int currentValue)
        {
            return currentValue + Value;
        }
    }
}
