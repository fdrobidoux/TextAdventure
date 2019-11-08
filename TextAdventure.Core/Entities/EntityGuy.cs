using System;
using System.Collections.Generic;
using SadConsole;
using SadConsole.Entities;
using SadConsole.Input;

namespace TextAdventure.Core.Entities
{
    public class EntityGuy : Entity
    {
        private readonly AnimatedConsole animIdle;
        private readonly AnimatedConsole animWalkRight;

        public EntityGuy(Font.FontSizes fontSize) : base(1, 1)
        {
            UseKeyboard = true;

            Font = Global.Fonts["EntityGuy"].GetFont(fontSize);

            animIdle = new AnimatedConsole("default", 1, 1, Font);
            animIdle.CreateFrame().SetGlyph(0, 0, Font.Master.GetGlyphDefinition("Idle").Glyph);
            this.Animations.Add("idle", animIdle);

            animWalkRight = new AnimatedConsole("walkRight", 1, 1, Font)
            {
                AnimationDuration = 0.5f,
                Repeat = true,
            };
            
            int[] allFrames = new[] { 3, 4, 5, 6, 7, 8, 9, 2 };
            for (int i = 0; i < allFrames.Length; i++)
            {
                CellSurface frame = animWalkRight.CreateFrame();
                frame.SetGlyph(0, 0, allFrames[i]);
            }

            Animations.Add("walkRight", animWalkRight);
            Animation = Animations["idle"];
            Animation.Start();

            //Font.Master.GetGlyphDefinition($"WalkRight{i}").Glyph
        }

        public override bool ProcessKeyboard(Keyboard info)
        {
            if (info.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Right))
            {
                Animation = Animations["walkRight"];
                Animation.Start();
            }
            else if (info.IsKeyReleased(Microsoft.Xna.Framework.Input.Keys.Right))
            {
                Animation.Stop();
            }

            return base.ProcessKeyboard(info);
        }

        public override void Update(TimeSpan timeElapsed)
        {
            if (UseKeyboard)
                ProcessKeyboard(SadConsole.Global.KeyboardState);

            base.Update(timeElapsed);
        }
    }
}
