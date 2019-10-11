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
using SadConsole.Themes;
using TextAdventure.Core.Console;

namespace TextAdventure
{
    public class TextGame : SadConsole.Game
    {
        ControlsConsole console;
        TestHealthConsole testHPConsole;

        HealthProgressBar HPBar;
        Label HPLabel;

        HealthBarTimelineEvent[] testEvents;

        public TextGame() : base("", 80, 25, null)
        {
            testEvents = new[] {
                new HealthBarTimelineEvent(0.5f, TimeSpan.FromSeconds(1)),
                new HealthBarTimelineEvent(0.15f, TimeSpan.FromSeconds(1.75)),
                new HealthBarTimelineEvent(0.25f, TimeSpan.FromSeconds(2.25)),
            };
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
            console.Add(HPLabel = new Label(80)
            {
                Position = new Point(0, 1)
            });

            SadConsole.Global.CurrentScreen = new SadConsole.ContainerConsole() {  };

            SadConsole.Global.CurrentScreen.Children.Add(console);

            testHPConsole = new TestHealthConsole(80, 20) { Position = new Point(0, 3) };
            testHPConsole.ClickAny += HPBar.TestHPConsole_ClickAny;
            SadConsole.Global.CurrentScreen.Children.Add(testHPConsole);
        }

        private void Program_WindowResized(object sender, EventArgs e)
        {
            SadConsole.Global.CurrentScreen.Resize(Global.WindowWidth / SadConsole.Global.CurrentScreen.Font.Size.X, Global.WindowHeight / SadConsole.Global.CurrentScreen.Font.Size.Y, true);
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