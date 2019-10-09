using System;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.EasingFunctions;
using XNAMathHelper = Microsoft.Xna.Framework.MathHelper;

namespace TextAdventure.Core.UI
{
    [DataContract]
    public partial class HealthProgressBar : SadConsole.Controls.ProgressBar
    {
        [DataMember]
        public bool freshDmgIsDecreasing => startDropTimer.IsPaused;

        [DataMember]
        protected float lastProgressValue;

        public float FreshDmgValue
        {
            get;
            set;
        }

        [DataMember]
        public int FreshDmgFillSize { get; set; }

        [DataMember]
        public TimeSpan FreshDmgDecrementAnimDuration { get; protected set; } = TimeSpan.FromSeconds(5); // TODO: Rename this field to be more precise (by timespan)

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
                FreshDmgValue = XNAMathHelper.Clamp(FreshDmgValue + diff, 0.0f, 1.0f);
            }
            else if (diff < 0.0f) // Negative
            {
                // decreased -
                FreshDmgValue = XNAMathHelper.Clamp(FreshDmgValue - diff, 0.0f, 1.0f);
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
            updateFreshDmgDblAnimation(time);
        }

        #region "TIMER - Start dropping --------------------------------------------------------------"

        public Timer startDropTimer;

        protected TimeSpan howLongUntilStartDropTimerTrigger = TimeSpan.FromMilliseconds(1000); // TODO: Change this as needed.

        private void createStartDropTimer()
        {
            if (startDropTimer == null)
            {
                startDropTimer = new Timer(howLongUntilStartDropTimerTrigger) { IsPaused = true, Repeat = false };
                startDropTimer.TimerElapsed += StartDropTimer_OnTimerElapsed;
            }
        }

        private void StartDropTimer_OnTimerElapsed(object sender, EventArgs e)
        {
            startFreshDmgDblAnimation();
            startDropTimer.IsPaused = true;
        }

        #endregion //                    --------------------------------------------------------------"

        #region "--- Ease out dropping ----------------------------------------------------------------"

        public DoubleAnimation freshDmgDblAnim;

        private void startFreshDmgDblAnimation()
        {
            freshDmgDblAnim = new DoubleAnimation()
            {
                Duration = FreshDmgDecrementAnimDuration,
                EasingFunction = new Expo() { Mode = EasingMode.Out },
                StartingValue = 1.0f,
                EndingValue = 0.0d
            };

            cumulMsTime = 0.0d;

            freshDmgDblAnim.Start();
        }

        double cumulMsTime = 0.0d;

        private void updateFreshDmgDblAnimation(TimeSpan time)
        {
            float currentValue;
            float calculatedFill;

            if (freshDmgDblAnim == null || !freshDmgDblAnim.IsStarted)
                return;

            if (freshDmgDblAnim.IsFinished)
            {
                FreshDmgFillSize = 0;
                return;
            }

            cumulMsTime += time.TotalMilliseconds;
            currentValue = (float)freshDmgDblAnim.GetValueForDuration(cumulMsTime);
            FreshDmgValue = currentValue;

            calculatedFill = CalcFreshDmgFillSize();
            FreshDmgFillSize = (int)(calculatedFill * currentValue);
        }

        #endregion //                  -------------------------------------------------------------

        private int CalcFreshDmgFillSize()
        {
            if (FreshDmgValue == 0) return 0;
            else if (FreshDmgValue == 1) return controlSize;
            else return (int)(controlSize * FreshDmgValue);
        }
    }
}
