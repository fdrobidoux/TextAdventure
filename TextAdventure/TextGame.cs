using System;
using System.Collections.Generic;
using System.Text;
using TextAdventure.Core.Concepts;
using SadConsole;
using SadConsole.Input;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;

namespace TextAdventure
{
    public class TextGame : SadConsole.Game
    {
        public TextGame() : base("", 80, 25, null)
        {
            Instance.Window.AllowAltF4 = false;
        }

        protected override void Initialize()
        {
            // Generally you don't want to hide the mouse from the user
            IsMouseVisible = true;

            Console console = new Console(80, 25);
            console.FillWithRandomGarbage();
            console.Fill(new Rectangle(3, 3, 23, 3), Color.Turquoise, Color.Black, 0, 0);
            console.Print(4, 4, "Test, oh wow it works !");

            SadConsole.Global.CurrentScreen.Children.Add(console);
        }

        protected override void Update(GameTime obj)
        {

        }

        protected override void Draw(GameTime obj)
        {

        }
    }
}