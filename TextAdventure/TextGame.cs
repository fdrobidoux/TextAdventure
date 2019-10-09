﻿using System;
using System.Collections.Generic;
using System.Text;
using TextAdventure.Core.Concepts;
using SadConsole;
using SadConsole.Input;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;
using TextAdventure.Core.UI;
using SadConsole.Controls;

namespace TextAdventure
{
    public class TextGame : SadConsole.Game
    {
        HealthProgressBar HPBar;
        Label HPLabel;
        Label AnotherLabel;
        bool done = false;

        public TextGame() : base("", 80, 25, null)
        {

        }

        protected override void Initialize()
        {
            // Generally you don't want to hide the mouse from the user
            IsMouseVisible = true;

            base.Initialize();

            ControlsConsole console = new SadConsole.ControlsConsole(80, 3);

            var consoleTheme = SadConsole.Themes.Library.Default.Clone();
            consoleTheme.ProgressBarTheme = new HealthProgressBarTheme();
            console.Theme = consoleTheme;

            console.Add(HPBar = new HealthProgressBar(console.Width, 1, HorizontalAlignment.Left)
            {
                Position = Point.Zero,
                Theme = new HealthProgressBarTheme()
            });
            console.Add(HPLabel = new Label(80) { Position = new Point(0, 1) });

            //Global.CurrentScreen = console;
            SadConsole.Global.CurrentScreen.Children.Add(console);
        }

        protected override void Update(GameTime gameTime)
        {
            if (!done && gameTime.TotalGameTime.Seconds == 1)
            {
                done = true;
                HPBar.Progress = 0.5f;
            }

            HPLabel.DisplayText = $"fDFS={HPBar.FreshDmgFillSize};fDV={HPBar.FreshDmgValue};";

            // Track stuff
            if (HPBar.freshDmgDblAnim != null)
            {
                
            }

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}