using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using SadConsole;

namespace TextAdventure.Core.UI
{
    internal class HealingCell : SadConsole.Entities.Entity
    {
        public HealingCell(AnimatedConsole animation) : base(animation)
        {
        }

        public HealingCell(int width, int height) : base(width, height)
        {
        }

        public HealingCell(int width, int height, Font font) : base(width, height, font)
        {
        }

        public HealingCell(Color foreground, Color background, int glyph) : base(foreground, background, glyph)
        {
        }
    }
}
