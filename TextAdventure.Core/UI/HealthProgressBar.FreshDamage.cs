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
        public FreshDamage FreshDmg { get; private set; }

        [DataMember]
        private float lastProgressValue;

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
            float muhDiff;

            dmgDoubleAnimation?.Reset();

            if (diff == 0.0f) return; // Shouldn't happen ever, but you never know.

            if (diff > 0.0f)
            {
                FreshDmgValue = XNAMathHelper.Clamp(FreshDmgValue - diff, 0.0f, 1.0f);
                FreshDmgFillSize = (int)(FreshDmgValue * Width);
            }
            else if (diff < 0.0f)
            {
                FreshDmgValue = XNAMathHelper.Clamp(FreshDmgValue - diff, 0.0f, 1.0f);
                FreshDmgFillSize = (int)(FreshDmgValue * Width);
            }

            lastProgressValue = progressValue;

            startDropTimer.Restart();
        }

        private void updateFreshDamageBar(TimeSpan time)
        {
            startDropTimer.Update(null, time);
            updateFreshDmgDblAnimation();
        }

        #region "TIMER - Start dropping ----------------------------------------------------------"

        public Timer startDropTimer;

        private void createStartDropTimer()
        {
            startDropTimer = new Timer(TimeSpan.FromTicks(1)) { IsPaused = true, Repeat = false };
            startDropTimer.TimerElapsed += StartDropTimer_OnTimerElapsed;
        }

        private void StartDropTimer_OnTimerElapsed(object sender, EventArgs e)
        {
            startFreshDmgDblAnimation();
        }

        #endregion // -----------------------------------------------------------------------------"

        #region "Ease out dropping ----------------------------------------------------------------"

        public DoubleAnimation dmgDoubleAnimation;
        public TimeSpan decrementAnimDuration = TimeSpan.FromSeconds(2);

        private void startFreshDmgDblAnimation()
        {
            dmgDoubleAnimation = new DoubleAnimation()
            {
                Duration = decrementAnimDuration,
                EasingFunction = new SadConsole.EasingFunctions.Circle() { Mode = EasingMode.Out },
            };

            if (FreshDmgValue >= 0.0f)
            {
                dmgDoubleAnimation.StartingValue = FreshDmgValue;
                dmgDoubleAnimation.EndingValue = 0.0d;
            }
            else if (FreshDmgValue <= 0.0f)
            {
                dmgDoubleAnimation.StartingValue = 0.0d;
                dmgDoubleAnimation.EndingValue = FreshDmgValue;
            }

            dmgDoubleAnimation.Start();
        }

        private void updateFreshDmgDblAnimation()
        {
            if (dmgDoubleAnimation == null || !dmgDoubleAnimation.IsStarted)
                return;

            FreshDmgValue = (float)dmgDoubleAnimation.CurrentValue;
            FreshDmgFillSize = (int)(controlSize * dmgDoubleAnimation.CurrentValue);
        }

        #endregion // -----------------------------------------------------------------------------"
    }
}
