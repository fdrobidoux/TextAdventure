﻿using System;
using System.Collections.Generic;
using System.Text;
using SadConsole;
using SadConsole.Input;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;
using TextAdventure.Core.UI;
using SadConsole.Controls;
using SadConsole.Themes;
using TextAdventure.Core.Console;

namespace TextAdventure
{
    public class TextGame : SadConsole.Game
    {
        ControlsConsole console;
        TestHealthConsole testHPConsole;

        HealthProgressBar HPBar;
        Label DmgLabel;

        public TextGame() : base("", 100, 50, null)
        {
            
        }

        protected override void Initialize()
        {
            // Generally you don't want to hide the mouse from the user
            IsMouseVisible = true;

            base.Initialize();

            console = new SadConsole.ControlsConsole(Global.CurrentScreen.Width, 3);

            console.Add(HPBar = new HealthProgressBar(console.Width, 1, HorizontalAlignment.Left)
            {
                Position = Point.Zero
            });
            console.Add(DmgLabel = new Label(80)
            {
                Position = new Point(0, 1)
            });

            SadConsole.Global.CurrentScreen.Children.Add(console);

            // Collection of buttons for testing.
            //testHPConsole = new TestHealthConsole(80, 20, HPBar) { Position = new Point(0, 3) };
            //testHPConsole.ClickAny += HPBar.TestHPConsole_ClickAny;

            SadConsole.Global.CurrentScreen.Children.Add(testHPConsole);
        }

        private void Program_WindowResized(object sender, EventArgs e)
        {
            SadConsole.Global.CurrentScreen.Resize(Global.WindowWidth / SadConsole.Global.CurrentScreen.Font.Size.X, Global.WindowHeight / SadConsole.Global.CurrentScreen.Font.Size.Y, true);
        }

        bool tempDone;

        protected override void Update(GameTime gameTime)
        {
            testHPConsole.Update(gameTime);

            if (!tempDone && gameTime.TotalGameTime.Seconds > 3)
            {
                tempDone = true;
            }

            DmgLabel.DisplayText = $"fDFS={HPBar.freshDmgFillSize};fDV={HPBar.FreshDmgValue};";
            DmgLabel.DisplayText = $"fDFS={HPBar.freshDmgFillSize};fDV={HPBar.FreshDmgValue};";

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}