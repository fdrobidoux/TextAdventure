using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SadConsole.Controls;
using TextAdventure.Core.UI;

namespace TextAdventure.Core.Consoles
{
    public class HUDConsole : SadConsole.ControlsConsole
    {
        Label HpLabel;
        HealthProgressBar HpProgressBar;

        public HUDConsole(int width, int height) : base(width, height)
        {
            HpLabel = new Label("Health:")
            {
                Position = new Point(1, 1), 
                CanFocus = false
            };

            HpProgressBar = new HealthProgressBar(width - 4, 2, SadConsole.HorizontalAlignment.Left)
            {
                Position = new Point(1, 2),
                CanFocus = false
            };
        }
    }
}
