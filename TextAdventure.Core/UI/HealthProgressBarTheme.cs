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

        public HealthProgressBarTheme() : base()
        {

        }

        public override void UpdateAndDraw(ControlBase control, TimeSpan time)
        {
            bool initialDirtiness = control.IsDirty;

            if (!(control is HealthProgressBar hpBar)) return;

            // Check dirtiness again from fresh damage stuff.

            if (!initialDirtiness) return;

            Rectangle fillRect;

            if (hpBar.IsHorizontal)
            {
                if (hpBar.HorizontalAlignment == HorizontalAlignment.Left)
                    fillRect = new Rectangle(hpBar.fillSize, 0, hpBar.FreshDmgFillSize, hpBar.Height);
                else
                    fillRect = new Rectangle((hpBar.Width - hpBar.fillSize), 0, hpBar.FreshDmgFillSize, hpBar.Height);
            }
            else
            {
                // TODO: Actual code; This won't work yet !!!!
                if (hpBar.VerticalAlignment == VerticalAlignment.Top)
                    fillRect = new Rectangle(0, 0, hpBar.Width, hpBar.fillSize);
                else
                    fillRect = new Rectangle(0, hpBar.Height - hpBar.fillSize, hpBar.Width, hpBar.fillSize);
            }

            Debug.WriteLine(fillRect.ToString());

            hpBar.Surface.Fill(fillRect, Color.Black, Color.White, 177, 0);

            base.UpdateAndDraw(control, time);
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

            FreshDamage = new Cell(themeColors.Black, themeColors.White);
        }
    }
}
