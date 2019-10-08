using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Controls;
using SadConsole.Themes;

namespace TextAdventure.Core.UI
{
    public class HealthProgressBarTheme : SadConsole.Themes.ProgressBarTheme
    {
        public HealthProgressBarTheme() : base()
        {
            this.SetBackground(Color.Red);
            this.SetForeground(Color.White);
            Colors = SadConsole.Themes.Library.Default.ProgressBarTheme.Colors;
        }

        public override void UpdateAndDraw(ControlBase control, TimeSpan time)
        {
            bool initialDirtiness = control.IsDirty;

            if (!(control is HealthProgressBar hpBar))
            {
                base.UpdateAndDraw(control, time);
                return;
            }

            base.UpdateAndDraw(control, time);

            if (!initialDirtiness) return;

            Rectangle fillRect;

            if (hpBar.IsHorizontal)
            {
                if (hpBar.HorizontalAlignment == HorizontalAlignment.Left)
                    fillRect = new Rectangle(hpBar.fillSize, 0, hpBar.freshDmgFillSize, hpBar.Height);
                else
                    fillRect = new Rectangle((hpBar.Width - hpBar.fillSize) + hpBar.freshDmgFillSize, 0, hpBar.freshDmgFillSize, hpBar.Height);
            }
            else
            {
                // TODO: Actual code; This won't work yet !!!!
                if (hpBar.VerticalAlignment == VerticalAlignment.Top)
                    fillRect = new Rectangle(0, 0, hpBar.Width, hpBar.fillSize);
                else
                    fillRect = new Rectangle(0, hpBar.Height - hpBar.fillSize, hpBar.Width, hpBar.fillSize);
            }

            hpBar.Surface.Fill(fillRect, Colors.Black, Colors.White, 178);
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
        }
    }
}
