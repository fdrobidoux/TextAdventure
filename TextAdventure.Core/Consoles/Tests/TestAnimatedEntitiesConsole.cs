using System;
using System.Collections.Generic;
using LINQ = System.Linq.Enumerable;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using SadConsole;
using SadConsole.Entities;
using SadConsole.Input;
using TextAdventure.Core.Entities;

namespace TextAdventure.Core.Consoles.Tests
{
    public class TestAnimatedEntitiesConsole : SadConsole.Console
    {
        Font characterFont;

        Entity stickman;

        public TestAnimatedEntitiesConsole() : base(30, 10)
        {
            UseKeyboard = true;

            Font = Global.Fonts["EntityGuy"].GetFont(Font.FontSizes.One);

            Children.Add(stickman = new EntityGuy(Font.SizeMultiple));
        }

        public override void Update(TimeSpan timeElapsed)
        {
            base.Update(timeElapsed);
        }
    }
}
