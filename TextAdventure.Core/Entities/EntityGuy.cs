using System;
using Microsoft.Xna.Framework.Input;
using Microsoft.Xna.Framework.Graphics;
using SadConsole;
using SadConsole.Entities;
using SadInput = SadConsole.Input;
using static SadConsole.FontMaster;
using Microsoft.Xna.Framework;

namespace TextAdventure.Core.Entities
{
    public class EntityGuy : Entity
    {
        private AnimatedConsole animIdle;
        private AnimatedConsole animWalkRight;
        private AnimatedConsole animWalkLeft;

        public EntityGuy(Font.FontSizes fontSize) : base(1, 1)
        {
            UseKeyboard = true;
            IsFocused = true;
            //UsePixelPositioning = true;

            Font = Global.Fonts["EntityGuy"].GetFont(fontSize);

            InitializeAnimations();
        }

        private void InitializeAnimations()
        {
            // Idle animation
            animIdle = new AnimatedConsole("default", 1, 1, Font);
            animIdle.CreateFrame().SetGlyph(0, 0, Font.Master.GetGlyphDefinition("Idle").Glyph);
            Animations.Add("Idle", animIdle);

            // Walk right animation
            animWalkRight = new AnimatedConsole("WalkRight", 1, 1, Font)
            {
                AnimationDuration = 0.5f,
                Repeat = true,
            };

            foreach (GlyphDefinition glyphDef in Font.Master.ListGlyphDefinitions("WalkRight"))
            {
                Cell singleCell = animWalkRight.CreateFrame().Cells[0];
                singleCell.Glyph = glyphDef.Glyph;
                singleCell.Mirror = glyphDef.Mirror;
            }
            Animations.Add("WalkRight", animWalkRight);

            animWalkLeft = new AnimatedConsole("WalkLeft", 1, 1, Font)
            {
                AnimationDuration = 0.5f,
                Repeat = true
            };
            foreach (GlyphDefinition glyphDef in Font.Master.ListGlyphDefinitions("WalkRight", SpriteEffects.FlipHorizontally))
            {
                Cell singleCell = animWalkLeft.CreateFrame().Cells[0];
                singleCell.Glyph = glyphDef.Glyph;
                singleCell.Mirror = glyphDef.Mirror;
            }
            Animations.Add("WalkLeft", animWalkLeft);
        }

        public override bool ProcessKeyboard(SadInput.Keyboard info)
        {
            // TODO use some StateMachine instead.

            if (info.IsKeyDown(Keys.Right))
            {
                Animation = Animations["WalkRight"];
            }
            else if (info.IsKeyDown(Keys.Left))
            {
                Animation = Animations["WalkLeft"];
            }
            else Animation = Animations["Idle"];

            Animation.Start();

            return base.ProcessKeyboard(info);
        }

        

        public override void Update(TimeSpan timeElapsed)
        {
            UpdateMovementRealWay(timeElapsed);

            base.Update(timeElapsed);
        }

        Vector2 moveVector = Vector2.Zero;

        private void UpdateMovementRealWay(TimeSpan timeElapsed)
        {
            KeyboardState keyboardState = Keyboard.GetState();

            if (keyboardState.IsKeyDown(Keys.Down))
                moveVector += Vector2.UnitY;
            else if (keyboardState.IsKeyDown(Keys.Up))
                moveVector -= Vector2.UnitY;

            if (keyboardState.IsKeyDown(Keys.Right))
                moveVector += Vector2.UnitX;
            else if (keyboardState.IsKeyDown(Keys.Left))
                moveVector -= Vector2.UnitX;

            Point newPosition = (moveVector * 5 * (float)timeElapsed.TotalSeconds).ToPoint();

            if (newPosition.Equals(Position)) return;

            // Do viewPosition.
            Position = newPosition;
        }
    }
}
