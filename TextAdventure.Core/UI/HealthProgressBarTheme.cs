using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Controls;
using SadConsole.Themes;

namespace TextAdventure.Core.UI
{
    public class HealthProgressBarTheme : SadConsole.Themes.ProgressBarTheme
    {
        public Cell FreshDamage;
        public Rectangle lastRect;
        private PreviousBarValues prevVals;

        public HealthProgressBarTheme() : base()
        {
        }

        public override void UpdateAndDraw(ControlBase control, TimeSpan time)
        {
            bool initialDirtiness = control.IsDirty;

            base.UpdateAndDraw(control, time);

            if ((control is HealthProgressBar hpBar))
                this.UpdateAndDraw_HealthProgressBar(hpBar, time, initialDirtiness);
        }

        public void UpdateAndDraw_HealthProgressBar(HealthProgressBar hpBar, TimeSpan time, bool InitialDirtiness)
        {
            if (!prevVals.Equals(hpBar))
            {
                prevVals = new PreviousBarValues(hpBar);

                lastRect = new Rectangle(hpBar.fillSize, 0, hpBar.FreshDmgFillSize, hpBar.Height);

                hpBar.IsDirty = true;
            }

            if (InitialDirtiness || hpBar.IsDirty)
                hpBar.Surface.Fill(lastRect, FreshDamage.Foreground, FreshDamage.Background, 178, 0);
        }

        public override void Attached(ControlBase control)
        {
            base.Attached(control);
        }

        public override ThemeBase Clone()
        {
            return base.Clone();
        }

        public override void RefreshTheme(Colors themeColors)
        {
            base.RefreshTheme(themeColors);

            FreshDamage = new Cell(themeColors.RedDark, themeColors.White);
        }

        private struct PreviousBarValues: IEquatable<HealthProgressBar>
        {
            public int freshDmgFillSize;
            public int fillSize;

            public PreviousBarValues(int freshDmgFillSize, int fillSize)
            {
                this.freshDmgFillSize = freshDmgFillSize;
                this.fillSize = fillSize;
            }

            public PreviousBarValues(HealthProgressBar other) : this(other.FreshDmgFillSize, other.fillSize) { }

            public bool Equals(HealthProgressBar other) 
                => (other.fillSize == this.fillSize) && (other.FreshDmgFillSize == this.freshDmgFillSize);

        }
    }
}
