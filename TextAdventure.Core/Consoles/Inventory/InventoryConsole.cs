using System;
using Microsoft.Xna.Framework;
using SadConsole;
using TextAdventure.Core.Glyphs;

namespace TextAdventure.Core.Consoles.Inventory
{
    public class InventoryConsole : SadConsole.Console
    {
        public InventoryConsole() : base(12, 6)
        {
            Font = SadConsole.Global.Fonts["InventorySprites"].GetFont(Font.FontSizes.Two);

            SetGlyph(0, 0, 16, Color.Magenta, Color.Transparent);
            SetGlyph(1, 1, 25, Color.Magenta, Color.Transparent);
            SetGlyph(0, 1, 24, Color.Magenta, Color.Transparent);
            
            SetGlyph(2, 0, InventorySprites.Sword2[0][0]);
            SetGlyph(2, 1, InventorySprites.Sword2[1][0]);
            SetGlyph(3, 1, InventorySprites.Sword2[1][1]);

            SetGlyph(0, 2, 36, Color.Red, Color.Transparent);
            SetDecorator(0, 2, 1, Font.Master.GetDecorator("runeBorder_Normal", Color.Teal));
        }

        protected override void OnVisibleChanged()
        {
            //if (IsVisible) this.RootChildren().MoveToTop(this);
            //else this.RootChildren().MoveToBottom(this);
            base.OnVisibleChanged();
        }
    }
}
