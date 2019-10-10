using System;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.EasingFunctions;
using TextAdventure.Core.Mechanics;
using XNAMathHelper = Microsoft.Xna.Framework.MathHelper;

namespace TextAdventure.Core.UI
{
    [DataContract]
    public partial class HealthProgressBar : SadConsole.Controls.ProgressBar
    {
        public FreshDamage FreshDmg { get; set; } = new FreshDamage();

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

            if (diff > 0.0f)
            {
                FreshDmgValue = XNAMathHelper.Clamp(FreshDmgValue + diff, 0.0f, 1.0f);
            }
            else if (diff < 0.0f)
            {
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
            startDropTimer.Update(null, time);
            updateFreshDmgDblAnimation();
        }

        #region "TIMER - Start dropping ----------------------------------------------------------"

        public Timer startDropTimer;

        protected TimeSpan howLongUntilStartDropTimerTrigger = TimeSpan.FromMilliseconds(250);

        private void createStartDropTimer()
        {
            startDropTimer = new Timer(howLongUntilStartDropTimerTrigger) { IsPaused = true, Repeat = false };
            startDropTimer.TimerElapsed += StartDropTimer_OnTimerElapsed;
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
                EasingFunction = new SadConsole.EasingFunctions.Linear() { Mode = EasingMode.InOut },
            };

            if (FreshDmgValue >= 0.0f)
            {
                dmgDoubleAnimation.StartingValue = FreshDmgValue;
                dmgDoubleAnimation.EndingValue = 0.0d;
            }
            else if (FreshDmgValue <= 0.0f)
            {
                dmgDoubleAnimation.StartingValue = FreshDmgValue;
                dmgDoubleAnimation.EndingValue = 0.0d;
            }

            dmgDoubleAnimation.Start();
        }

        private void updateFreshDmgDblAnimation()
        {
            if (dmgDoubleAnimation == null || !dmgDoubleAnimation.IsStarted)
                return;

            if (dmgDoubleAnimation.IsFinished)
            {
                FreshDmgFillSize = 0;
                return;
            }

            FreshDmgValue = (float)dmgDoubleAnimation.CurrentValue;
            FreshDmgFillSize = (int)(controlSize * FreshDmgValue);
        }

        #endregion // -----------------------------------------------------------------------------"
    }
}
