using System;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.EasingFunctions;
using XNAMathHelper = Microsoft.Xna.Framework.MathHelper;

namespace TextAdventure.Core.UI
{
    public partial class HealthProgressBar : SadConsole.Controls.ProgressBar
    {
        [DataMember]
        protected float freshDmgValue;

        [DataMember]
        public int freshDmgFillSize;

        [DataMember]
        public bool freshDmgIsDecreasing => startDropTimer.IsPaused;

        [DataMember]
        protected float lastProgressValue;

        public float FreshDmgValue
        {
            get => freshDmgValue;
            set {
                freshDmgValue = value;
            }
        }

        [DataMember]
        public TimeSpan FreshDmgDecrementAnimDuration { get; protected set; } // TODO: Rename this field to be more precise (by timespan)

        private void bindFreshDmgOnProgressChanged()
        {
            lastProgressValue = progressValue;
            ProgressChanged += HealthProgressBar_ProgressChanged;
        }

        private void HealthProgressBar_ProgressChanged(object sender, EventArgs e)
        {
            var diff = progressValue - lastProgressValue;

            if (diff == 0.0f)
                return;

            if (diff > 0.0f) // Positive
            {
                // increased +
                freshDmgValue = XNAMathHelper.Clamp(freshDmgValue + diff, 0.0f, 1.0f);
            }
            else if (diff < 0.0f) // Negative
            {
                // decreased -
                freshDmgValue = XNAMathHelper.Clamp(freshDmgValue + diff, 0.0f, 1.0f);
            }

            // Assign current value as last value checked.
            lastProgressValue = progressValue;

            // Reset timer for doing the eased-out visual decrease.
            startDropTimer.TimerAmount = howLongUntilStartDropTimerTrigger;
            startDropTimer.Restart();
        }

        private void updateFreshDamageBar(TimeSpan time)
        {
            startDropTimer.Update(Parent, time);
            updateFreshDmgDblAnimation();
        }

#region "TIMER - Start dropping"

        public Timer startDropTimer;

        protected TimeSpan howLongUntilStartDropTimerTrigger = TimeSpan.FromSeconds(1); // TODO: Change this as needed.

        private void createStartDropTimer()
        {
            if (startDropTimer == null)
            {
                startDropTimer = new Timer(howLongUntilStartDropTimerTrigger) { IsPaused = true };
                startDropTimer.TimerElapsed += StartDropTimer_OnTimerElapsed;
            }
        }

        private void StartDropTimer_OnTimerElapsed(object sender, EventArgs e)
        {
            startFreshDmgDblAnimation();
        }

#endregion

#region "Ease out dropping"

        public DoubleAnimation freshDmgDblAnim;

        private void startFreshDmgDblAnimation()
        {
            freshDmgDblAnim = new DoubleAnimation()
            {
                Duration = FreshDmgDecrementAnimDuration,
                EasingFunction = new Expo() { Mode = EasingMode.Out },
                StartingValue = freshDmgValue,
                EndingValue = 0.0d
            };

            freshDmgDblAnim.Start();
        }

        private void updateFreshDmgDblAnimation()
        {
            float currentValue;
            float calculatedFill;

            if (freshDmgDblAnim == null || !freshDmgDblAnim.IsStarted)
                return;

            if (freshDmgDblAnim.IsFinished)
            {
                freshDmgFillSize = 0;
                freshDmgValue = 0.0f;
                return;
            }

            currentValue = (float)freshDmgDblAnim.CurrentValue;
            freshDmgValue = currentValue;

            calculatedFill = CalcFreshDmgFillSize();
            freshDmgFillSize = (int)(calculatedFill - (calculatedFill * currentValue));
        }

#endregion

        private int CalcFreshDmgFillSize()
        {
            if (freshDmgValue == 0) return 0;
            else if (freshDmgValue == 1) return controlSize;
            else return (int)(controlSize * freshDmgValue);
        }
    }
}
