using System;
using System.Collections.Generic;
using System.Text;
using SadConsole;
using SadConsole.Input;
using Microsoft.Xna.Framework;
using XNAInput = Microsoft.Xna.Framework.Input;
using TextAdventure.Core.Consoles.Tests;
using TextAdventure.Core.Consoles.Inventory;

namespace TextAdventure
{
    public class TextGame : SadConsole.Game
    {
        InventoryConsole myInventoryConsole;

        public TextGame() : base("", 120, 40, null)
        {
            // Generally you don't want to hide the mouse from the user
            IsMouseVisible = true;
        }

        protected override void Initialize()
        {
            base.Initialize();

            // Load fonts.
            SadConsole.Global.LoadFont("Content/fonts/InventorySprites_16x16.font");
            SadConsole.Global.LoadFont("Content/fonts/EntityGuy.font");
            
            Add(new MainTestSelectorConsole());
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