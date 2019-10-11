using System;
using System.Collections.Generic;
using System.Diagnostics;
using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Controls;
using SadConsole.Themes;
using TextAdventure.Core.UI;

namespace TextAdventure.Core.Themes
{
    public class HealthProgressBarTheme : SadConsole.Themes.ProgressBarTheme
    {
        public Cell FreshDamage;
        public Cell Healing;

        public Rectangle dmgRect;
        public Rectangle healRect;
        private PreviousStateEncapsulator previousState;

        public HealthProgressBarTheme() : base()
        {
        }

        public override void UpdateAndDraw(ControlBase control, TimeSpan time)
        {
            bool initialDirtiness = control.IsDirty;

            base.UpdateAndDraw(control, time);

            if ((control is HealthProgressBar hpBar))
            {
                this.UpdateAndDraw_FreshDamage(hpBar, time, initialDirtiness);
                //this.UpdateAndDraw_Healing(hpBar, time, initialDirtiness);
            }
        }

        public void UpdateAndDraw_FreshDamage(HealthProgressBar hpBar, TimeSpan time, bool InitialDirtiness)
        {
            InitialDirtiness &= hpBar.freshDmgFillSize > 0;

            if (previousState.freshDmgFillSize != hpBar.freshDmgFillSize || previousState.fillSize != hpBar.fillSize)
            {
                previousState = new PreviousStateEncapsulator(hpBar);
                dmgRect = new Rectangle(hpBar.fillSize, 0, hpBar.freshDmgFillSize, hpBar.Height);
                hpBar.IsDirty = true;
            }

            if (InitialDirtiness || hpBar.IsDirty)
                hpBar.Surface.Fill(dmgRect, FreshDamage.Foreground, FreshDamage.Background, 178, 0);
        }

        private void UpdateAndDraw_Healing(HealthProgressBar hpBar, TimeSpan time, bool initialDirtiness)
        {
            if (previousState.healingFillSize != hpBar.healingFillSize)
            {
                previousState = new PreviousStateEncapsulator(hpBar);
                healRect = new Rectangle(hpBar.fillSize - hpBar.healingFillSize, 0, hpBar.healingFillSize, hpBar.Height);
                hpBar.Surface.Fill(dmgRect, Healing.Foreground, Healing.Background, 17, 0);
                hpBar.IsDirty = true;
            }
        }

        public override void Attached(ControlBase control) => base.Attached(control);

        public override ThemeBase Clone()
        {
            return base.Clone();
        }

        public override void RefreshTheme(Colors themeColors)
        {
            base.RefreshTheme(themeColors);

            FreshDamage = new Cell(themeColors.RedDark, themeColors.Red);
            Healing = new Cell(themeColors.Green, themeColors.Gold);
        }

        private struct PreviousStateEncapsulator: IEquatable<HealthProgressBar>
        {
            public int freshDmgFillSize;
            public int fillSize;
            public int healingFillSize;

            public PreviousStateEncapsulator(int fillSize, int freshDmgFillSize, int healingFillSize)
            {
                this.fillSize = fillSize;
                this.freshDmgFillSize = freshDmgFillSize;
                this.healingFillSize = healingFillSize;
            }

            public PreviousStateEncapsulator(HealthProgressBar hpb) : this(hpb.fillSize, hpb.freshDmgFillSize, hpb.healingFillSize) { }

            public bool Equals(HealthProgressBar other)
            {
                return (other.fillSize == this.fillSize) && (other.freshDmgFillSize == this.freshDmgFillSize);
            }

        }
    }
}
