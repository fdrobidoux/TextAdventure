using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using SadConsole;
using SadConsole.Entities;

namespace TextAdventure.Core.Consoles.Tests
{
    public class TestAnimatedEntitiesConsole : SadConsole.Console
    {
        public TestAnimatedEntitiesConsole() : base(Global.CurrentScreen.Width, Global.CurrentScreen.Height)
        {
            Font = Global.Fonts["EntityGuy"].GetFont(Font.FontSizes.One);

            AnimatedConsole animWalkRight = new AnimatedConsole("WalkRight", 1, 1, Font);

            for (int i = 2; i < 10; i++)
            {
                CellSurface frame = animWalkRight.CreateFrame();
                frame.Cells[0].Glyph = i;
                frame.Cells[0].Mirror = SpriteEffects.FlipHorizontally;
            }

            Entity stickman = new Entity(1, 1, Font);
            stickman.Animations.Add("WalkRight", animWalkRight);

            stickman.Animation = stickman.Animations["WalkRight"];
        }
    }
}
