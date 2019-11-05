using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventure.RpgMechanics.Calculations
{
    public abstract class ProgressStat<T> : Stat<T> where T : struct, IComparable
    {
        public readonly bool _willTrackOverflow = false;

        private T _maximumValue;

        public override T Value
        {
            get => _currentValue;
            protected set {
                T keptCurrentValue = _currentValue;

                if (value.CompareTo(MaximumValue) > 0)
                {
                    base.Value = _maximumValue;

                    if (_willTrackOverflow)
                    {
                        T diff = default;
                        dynamic tempCVal = value;
                        dynamic tempMax = MaximumValue;

                        try
                        {
                            diff = (T)(tempCVal - tempMax);
                        }
                        catch (ArithmeticException e)
                        {
                            // TODO: Remove me when I'm done.
                            System.Diagnostics.Debugger.Break();
                        }
                        finally
                        {
                            OverflowingValue?.Invoke(this, new OverflowingValueEventArgs<T>(diff));
                        }
                    }
                }
                else
                {
                    base.Value = value;
                }
            }
        }

        public T MaximumValue { get => _maximumValue; set => _maximumValue = value; }

        public ProgressStat(T maxValue, T startingValue, bool willTrackOverflow = false) : base()
        {
            _willTrackOverflow = willTrackOverflow;
            _maximumValue = maxValue;
            _currentValue = startingValue;
        }

        public event OverflowingValueEventHandler OverflowingValue;
        public delegate void OverflowingValueEventHandler(object sender, OverflowingValueEventArgs<T> e);
    }

    public class OverflowingValueEventArgs<T> : EventArgs where T : struct, IComparable
    {
        public T amountOverflowing { get; set; }

        public OverflowingValueEventArgs(T amountOverflowing)
        {
            this.amountOverflowing = amountOverflowing;
        }
    }
}
