using System;
using System.Collections.Generic;
using System.Text;
using System.Reflection;
using System.Linq.Expressions;

namespace TextAdventure.RpgMechanics.Calculations
{
    public abstract class Stat<T> : IDisposable where T : struct, IComparable
    {
        protected List<Modifier<T>> modifiers;

        protected T _currentValue;

        public virtual T Value
        {
            get => _currentValue; 
            protected set {
                
                T oldValue = _currentValue;
                int comparedWithPrevious = _currentValue.CompareTo(oldValue);

                if (comparedWithPrevious == 0) 
                    return;

                _currentValue = value;

                ValueChanged?.Invoke(this, new ValueChangedEventArgs<T>(oldValue));
            }
        }

        public T ProcessModifiers()
        {
            T currentValue = Value;

            foreach (Modifier<T> mod in modifiers)
            {
                currentValue = mod.Modify(currentValue);
            }

            return currentValue;
        }

        public event ValueChangedEventHandler ValueChanged;
        public delegate void ValueChangedEventHandler(object sender, ValueChangedEventArgs<T> e);

        #region IDisposable Support
        private bool disposedValue = false; // To detect redundant calls

        protected virtual void Dispose(bool disposing)
        {
            if (!disposedValue)
            {
                if (disposing)
                {
                    // TODO: dispose managed state (managed objects).
                }

                // TODO: free unmanaged resources (unmanaged objects) and override a finalizer below.
                foreach (Delegate d in ValueChanged.GetInvocationList())
                    ValueChanged -= (ValueChangedEventHandler)d;

                // TODO: set large fields to null.
                
                disposedValue = true;
            }
        }

        // This code added to correctly implement the disposable pattern.
        public void Dispose()
        {
            Dispose(true);
        }

        #endregion
    }

    public class ValueChangedEventArgs<T> : EventArgs where T : struct, IComparable
    {
        public T PreviousValue { get; set; }

        public ValueChangedEventArgs(T previousValue)
        {
            PreviousValue = previousValue;
        }
    }
}
