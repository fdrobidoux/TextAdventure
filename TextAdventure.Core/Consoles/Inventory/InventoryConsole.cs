using System;
using Microsoft.Xna.Framework;
using SadConsole;

namespace TextAdventure.Core.Consoles.Inventory
{
    public class InventoryConsole : SadConsole.Console
    {
        public InventoryConsole() : base(12, 6)
        {
            Font = SadConsole.Global.Fonts["InventorySprites"].GetFont(Font.FontSizes.One);

            SetGlyph(0, 0, 16, Color.Magenta, Color.Transparent);
            SetGlyph(0, 1, 24, Color.Magenta, Color.Transparent);
            SetGlyph(1, 1, 25, Color.Magenta, Color.Transparent);
            
            SetGlyph(2, 0, "sword1@0,0");
            SetGlyph(2, 1, "sword1@0,1");
            SetGlyph(3, 1, "sword1@1,1");
        }

        protected override void OnVisibleChanged()
        {
            base.OnVisibleChanged();
        }
    }
}
