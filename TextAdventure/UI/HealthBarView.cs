using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SadConsole;
using Console = SadConsole.Console;
using SadConsole.Controls;
using TextAdventure.Core.UI;

namespace TextAdventure.UI
{
    public class HealthBarView : ControlsConsole
    {
        

        Color healthBarColor = Color.IndianRed;
        Color healthTextColor = Color.AntiqueWhite;
        
        ProgressBar healthBar;
        Label healthLabel;

        public HealthBarView(Console parent) : base(parent.Width, 4)
        {
            this.Theme = this.DefineTheme();

            // Health label
            this.Add(healthLabel = new SadConsole.Controls.Label("Health"));

            // Health remaining bar
            this.Add(healthBar = new HealthProgressBar(parent.Width - 4, 1, HorizontalAlignment.Stretch)
            {
                Position = new Point(2, 2)
            });
        }

        private SadConsole.Themes.Library DefineTheme()
        {
            var consoleTheme = SadConsole.Themes.Library.Default.Clone();
            consoleTheme.ProgressBarTheme = new HealthProgressBarTheme();



            return consoleTheme;
        }

        public override void Draw(TimeSpan timeElapsed)
        {
            base.Draw(timeElapsed);
        }

        public override void OnCalculateRenderPosition()
        {
            base.OnCalculateRenderPosition();
        }

        public override void Update(TimeSpan timeElapsed)
        {
            base.Update(timeElapsed);
        }

        protected override void OnParentChanged(Console oldParent, Console newParent)
        {
            base.OnParentChanged(oldParent, newParent);
        }
    }
}
