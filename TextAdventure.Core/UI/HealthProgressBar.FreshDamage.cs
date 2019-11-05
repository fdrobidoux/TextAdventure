using System;
using System.Linq;
using System.Runtime.Serialization;
using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.EasingFunctions;
#if NETSTANDARD2_0
using MathHelper = SadConsole.MathHelper;
#else
using MathHelper = Microsoft.Xna.Framework.MathHelper;
#endif

namespace TextAdventure.Core.UI
{
    [DataContract]
    public partial class HealthProgressBar : SadConsole.Controls.ProgressBar
    {
        public float lastProgressDecrement { get; private set; }

        public int FreshDmgFillSize { get; set; }
        public float FreshDmgValue { get; set; }

        private void bindFreshDmgOnProgressChanged()
        {
            lastProgressDecrement = progressValue;
            ProgressChanged += FreshDamage_ProgressChanged;
        }

        private void FreshDamage_ProgressChanged(object sender, EventArgs e)
        {
            var diff = progressValue - lastProgressDecrement;

            dmgDoubleAnimation?.Reset();

            FreshDmgValue = MathHelper.Clamp(FreshDmgValue - diff, 0.0f, 1.0f);
            FreshDmgFillSize = (int)(controlSize * FreshDmgValue);

            lastProgressDecrement = progressValue;

            //startDropTimer.Restart();
        }

        private void updateFreshDamageBar(TimeSpan time)
        {
            //startDropTimer.Update(null, time);
            //updateFreshDmgDblAnimation();
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
        public TimeSpan decrementAnimDuration = TimeSpan.FromSeconds(3);

        private void startFreshDmgDblAnimation()
        {
            dmgDoubleAnimation = new DoubleAnimation()
            {
                Duration = decrementAnimDuration,
                EasingFunction = new SadConsole.EasingFunctions.Expo() { Mode = EasingMode.Out },
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
            FreshDmgFillSize = (int)(controlSize * FreshDmgValue);
            if (FreshDmgFillSize <= 0)
                dmgDoubleAnimation.Duration = TimeSpan.FromTicks(0);
        }

#endregion // -----------------------------------------------------------------------------"
    }
}
