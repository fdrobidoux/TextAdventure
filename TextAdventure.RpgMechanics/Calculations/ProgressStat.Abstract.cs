using System;
using System.Collections.Generic;
using System.Text;

namespace TextAdventure.RpgMechanics.Calculations
{
    public class ProgressStat<T> : Stat<T> where T : struct
    {
        private T _currentValue;
        public T CurrentValue { 
            get => _currentValue; 
            set {
                var oldValue = _currentValue;
                _currentValue = value;
                ValueChanged?.Invoke(this, new ValueChangedEventArgs<T>(oldValue));
            }
        }

        public T MaximumValue { get; set; }

        public ProgressStat() : base()
        {

        }

        public event ValueChangedEventHandler ValueChanged;

        public delegate void ValueChangedEventHandler(object sender, ValueChangedEventArgs<T> e);
    }

    public class ValueChangedEventArgs<T> : EventArgs where T : struct
    {
        public T PreviousValue { get; set; }

        public ValueChangedEventArgs(T previousValue)
        {
            PreviousValue = previousValue;
        }
    }
}
