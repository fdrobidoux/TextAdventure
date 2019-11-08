using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework.Graphics;
using SadConsole;
using SadConsole.Entities;

namespace TextAdventure.Core.Consoles.Tests
{
    public class TestAnimatedEntitiesConsole : SadConsole.Console
    {
        public TestAnimatedEntitiesConsole() : base(30, 10)
        {
            Font = Global.Fonts["EntityGuy"].GetFont(Font.FontSizes.Two);

            SetGlyph(0, 0, 3);

            AnimatedConsole animIdle = new AnimatedConsole("default", 1, 1, Font);

            animIdle.CreateFrame().Cells[0].Glyph = 2;

            AnimatedConsole animWalkRight = new AnimatedConsole("WalkRight", 1, 1, Font);

            for (int i = 2; i < 11; i++)
            {
                CellSurface frame = animWalkRight.CreateFrame();
                frame.Cells[0].Glyph = i;
                frame.Cells[0].Mirror = SpriteEffects.FlipHorizontally;
            }

            Entity stickman = new Entity(animIdle);
            stickman.Animations.Add("WalkRight", animWalkRight);

            stickman.Animation = animIdle;

            Children.Add(stickman);
        }
    }
}
