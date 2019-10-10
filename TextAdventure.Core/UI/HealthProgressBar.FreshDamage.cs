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

        [DataMember]
        public float FreshDmgValue { get; set; }

        [DataMember]
        public int FreshDmgFillSize { get; set; }

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

            FreshDmgFillSize = (int)(FreshDmgValue * Width);

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

        #region "TIMER - Start dropping ----------------------------------------------------------"

        public Timer startDropTimer;

        protected TimeSpan howLongUntilStartDropTimerTrigger = TimeSpan.FromMilliseconds(250);

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

        #endregion // -----------------------------------------------------------------------------"

        #region "Ease out dropping ----------------------------------------------------------------"

        public DoubleAnimation dmgDoubleAnimation;
        public TimeSpan decrementAnimDuration = TimeSpan.FromSeconds(1);

        private void startFreshDmgDblAnimation()
        {
            dmgDoubleAnimation = new DoubleAnimation()
            {
                Duration = decrementAnimDuration,
                EasingFunction = new SadConsole.EasingFunctions.Linear(),
                StartingValue = FreshDmgValue,
                EndingValue = 0.0d
            };

            dmgDoubleAnimation.Start();
        }

        private void updateFreshDmgDblAnimation()
        {
            if (dmgDoubleAnimation == null || !dmgDoubleAnimation.IsStarted || dmgDoubleAnimation.IsFinished)
                return;

            FreshDmgValue = (float)dmgDoubleAnimation.CurrentValue;
            FreshDmgFillSize = (int)(controlSize * FreshDmgValue);
        }

        #endregion // -----------------------------------------------------------------------------"

        #region "Ease out incrementing ------------------------------------------------------------"



        #endregion // -----------------------------------------------------------------------------"
    }
}
