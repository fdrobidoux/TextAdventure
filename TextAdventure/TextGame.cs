using System;
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

        HealthBarTimelineEvent[] testEvents;

        public TextGame() : base("", 80, 25, null)
        {
            testEvents = new[] {
                new HealthBarTimelineEvent(0.5f, TimeSpan.FromSeconds(1)),
                new HealthBarTimelineEvent(0.25f, TimeSpan.FromMilliseconds(1500)),
                new HealthBarTimelineEvent(0.75f, TimeSpan.FromSeconds(3)),
            };
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
            foreach (HealthBarTimelineEvent _event in testEvents)
            {
                if (!_event.isDone && gameTime.TotalGameTime >= _event.when)
                {
                    _event.isDone = true;
                    HPBar.Progress = _event.newValue;
                }
            }

            HPLabel.DisplayText = $"fDFS={HPBar.FreshDmgFillSize};fDV={HPBar.FreshDmgValue};";

            base.Update(gameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }

    internal class HealthBarTimelineEvent
    {
        public float newValue;
        public TimeSpan when;
        public bool isDone = false;

        public HealthBarTimelineEvent(float @newValue, TimeSpan @when)
        {
            this.newValue = @newValue;
            this.when = @when;
        }
    }
}