using System;
using System.Collections.Generic;
using System.Text;
using SadConsole;
using SadConsole.Input;
using Console = SadConsole.Console;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using TextAdventure.Core.UI;
using SadConsole.Controls;
using SadConsole.Themes;
using TextAdventure.Core.Console;
using TextAdventure.Core.Consoles;
using TextAdventure.Core.Consoles.Inventory;

namespace TextAdventure
{
    public class TextGame : SadConsole.Game
    {
        HUDConsole MyHUDConsole;
        InventoryConsole MyInventoryConsole;
        
        TestHealthConsole someTestHealthConsole;
        TestHealthConsole someTestManaConsole;

        public TextGame() : base("", 120, 40, null)
        {
            
        }

        protected override void Initialize()
        {
            // Generally you don't want to hide the mouse from the user
            IsMouseVisible = true;

            base.Initialize();

            SadConsole.Global.LoadFont("Content/fonts/InventorySprites_16x16.font");

            Add(MyHUDConsole = new HUDConsole(Global.CurrentScreen.Width, 4));

            // Collection of buttons for testing.
            Add(someTestHealthConsole = new TestHealthConsole(80, 20, MyHUDConsole.HpProgressBar) { Position = new Point(0, 5) });
            Add(someTestManaConsole = new TestHealthConsole(80, 20, MyHUDConsole.ManaProgressBar) { Position = new Point(30, 5) });

            MyInventoryConsole = new InventoryConsole() { Position = new Point(0, 16) };
            Add(MyInventoryConsole);

            MyHUDConsole.InventoryBtn.Click += InventoryBtn_Click;
        }

        private void InventoryBtn_Click(object sender, EventArgs e)
        {
            MyInventoryConsole.IsVisible = !MyInventoryConsole.IsVisible;
        }

        private void Add(Console screen) 
            => Global.CurrentScreen.Children.Add(screen);

        [Obsolete]
        private void Program_WindowResized(object sender, EventArgs e)
        {
            SadConsole.Global.CurrentScreen.Resize(Global.WindowWidth / SadConsole.Global.CurrentScreen.Font.Size.X, Global.WindowHeight / SadConsole.Global.CurrentScreen.Font.Size.Y, true);
        }

        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);
            someTestHealthConsole.Update(gameTime.ElapsedGameTime);
            someTestManaConsole.Update(gameTime.ElapsedGameTime);
        }

        protected override void Draw(GameTime gameTime)
        {
            base.Draw(gameTime);
        }
    }
}