using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.EasingFunctions;
using System;
using System.Collections.Generic;
using System.Text;
using MathHelper = SadConsole.MathHelper;

namespace TextAdventure.Core.UI
{
    public partial class HealthProgressBar : SadConsole.Controls.ProgressBar
    {
        private float lastProgressIncrement;
        public int healingFillSize;

        public float HealingValue { get; set; }

        private void bindHealingOnProgressChanged()
        {
            lastProgressIncrement = 0.0f;
            ProgressChanged += Healing_ProgressChanged;
        }

        private void Healing_ProgressChanged(object sender, EventArgs e)
        {
            var diff = lastProgressIncrement - progressValue;

            if (diff == 0.0f) return; // Shouldn't happen ever, but you never know.

            HealingValue = MathHelper.Clamp(HealingValue + diff, 0.0f, 1.0f);
            healingFillSize = (int)(HealingValue * Width);

            lastProgressIncrement = progressValue;

            fadeOutHealingDblAnimation?.Reset();
        }

        #region "Ease out fading color ----------------------------------------------------------------"

        public DoubleAnimation fadeOutHealingDblAnimation;
        public TimeSpan fadeOutHealingDuration = TimeSpan.FromSeconds(1);
        public Color currentColor;

        private readonly Color STARTING_COLOR = Color.Green;

        [Obsolete]
        private void startHealingDblAnimation()
        {
            fadeOutHealingDblAnimation = new DoubleAnimation()
            {
                Duration = fadeOutHealingDuration,
                EasingFunction = new SadConsole.EasingFunctions.Circle() { Mode = EasingMode.Out },
                StartingValue = 1.0,
                EndingValue = 0.0
            };

            currentColor = STARTING_COLOR;

            fadeOutHealingDblAnimation.Start();
        }

        [Obsolete]
        private void updateHealingDblAnimation()
        {
            if (fadeOutHealingDblAnimation == null || !fadeOutHealingDblAnimation.IsStarted)
                return;
        }

        #endregion
    }
}
