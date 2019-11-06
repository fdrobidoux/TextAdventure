using System;
using System.Collections.Generic;
using System.Text;
using SadConsole;
using SadConsole.Input;
using Microsoft.Xna.Framework;
using XNAInput = Microsoft.Xna.Framework.Input;
using TextAdventure.Core.Console;
using TextAdventure.Core.Consoles;
using TextAdventure.Core.Consoles.Inventory;

namespace TextAdventure
{
    public class TextGame : SadConsole.Game
    {
        HUDConsole myHUDConsole;
        InventoryConsole myInventoryConsole;
        
        TestHealthConsole someTestHealthConsole;
        TestHealthConsole someTestManaConsole;

        TestPixelOffsetConsole myTestPixelOffsetConsole;

        public TextGame() : base("", 120, 40, null)
        {
            
        }

        protected override void Initialize()
        {
            // Generally you don't want to hide the mouse from the user
            IsMouseVisible = true;

            base.Initialize();

            SadConsole.Global.LoadFont("Content/fonts/InventorySprites_16x16.font");

            Add(myHUDConsole = new HUDConsole(Global.CurrentScreen.Width, 4));

            // Collection of buttons for testing.
            Add(someTestHealthConsole = new TestHealthConsole(80, 20, myHUDConsole.HpProgressBar) { Position = new Point(0, 5) });
            Add(someTestManaConsole = new TestHealthConsole(80, 20, myHUDConsole.ManaProgressBar) { Position = new Point(30, 5) });

            Add(myInventoryConsole = new InventoryConsole() { Position = new Point(0, 16) });
            myHUDConsole.InventoryBtn.Click += (s, e) => myInventoryConsole.IsVisible = !myInventoryConsole.IsVisible;

            Add(myTestPixelOffsetConsole = new TestPixelOffsetConsole(10, 5));
        }

        private void Add(SadConsole.Console screen) 
            => Global.CurrentScreen.Children.Add(screen);

        [Obsolete]
        private void Program_WindowResized(object sender, EventArgs e)
        {
            SadConsole.Global.CurrentScreen.Resize(Global.WindowWidth / SadConsole.Global.CurrentScreen.Font.Size.X, Global.WindowHeight / SadConsole.Global.CurrentScreen.Font.Size.Y, true);
        }


        protected override void Update(GameTime gameTime)
        {
            base.Update(gameTime);

            
        }
    }
}