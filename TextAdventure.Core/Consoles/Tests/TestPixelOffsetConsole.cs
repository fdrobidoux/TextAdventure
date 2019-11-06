using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using XnaInput = Microsoft.Xna.Framework.Input;
using SadConsole;

namespace TextAdventure.Core.Consoles
{
    public class TestPixelOffsetConsole : SadConsole.Console
    {
        SadConsole.Console RealPositionConsole;
        SadConsole.Console ViewPositionConsole;

        public TestPixelOffsetConsole(int width, int height) : base(width, height, Global.Fonts["InventorySprites"].GetFont(Font.FontSizes.One))
        {
            UsePixelPositioning = true;

            RealPositionConsole = new SadConsole.Console(Width, Height, Font);
            RealPositionConsole.SetGlyph(0, 0, 2, Color.OldLace);
            RealPositionConsole.SetGlyph(0, 1, 2, Color.OldLace);
            RealPositionConsole.SetGlyph(1, 0, 2, Color.OldLace);
            RealPositionConsole.SetGlyph(1, 1, 2, Color.OldLace);
            Children.Add(RealPositionConsole);

            ViewPositionConsole = new SadConsole.Console(Width, Height, Font) { UsePixelPositioning = true };
            ViewPositionConsole.SetGlyph(0, 0, 2, Color.Gray);
            ViewPositionConsole.SetGlyph(0, 1, 2, Color.White);
            ViewPositionConsole.SetGlyph(1, 0, 2, Color.Blue);
            ViewPositionConsole.SetGlyph(1, 1, 2, Color.Red);
            Children.Add(ViewPositionConsole);
        }

        public override void Draw(TimeSpan timeElapsed)
        {
            base.Draw(timeElapsed);
        }

        public override void Update(TimeSpan timeElapsed)
        {
            base.Update(timeElapsed);
            UpdateKeyboard(timeElapsed);
            //UpdateMouse();
        }
        
        Vector2 movementVector = Vector2.Zero;

        private void UpdateKeyboard(TimeSpan timeElapsed)
        {
            XnaInput.KeyboardState keyboardState = XnaInput.Keyboard.GetState();

            if (keyboardState.IsKeyDown(XnaInput.Keys.Down))
                movementVector += Vector2.UnitY;
            else if (keyboardState.IsKeyDown(XnaInput.Keys.Up))
                movementVector -= Vector2.UnitY;

            if (keyboardState.IsKeyDown(XnaInput.Keys.Right))
            {
                movementVector += Vector2.UnitX;
                //this.ShiftRight();
            }
            else if (keyboardState.IsKeyDown(XnaInput.Keys.Left))
            {
                movementVector -= Vector2.UnitX;
                //this.ShiftLeft();
            }

            Point newPosition = (movementVector * (float)timeElapsed.TotalSeconds * 25).ToPoint();

            if (newPosition.Equals(ViewPositionConsole.Position)) return;

            // Do viewPosition.
            ViewPositionConsole.Position = newPosition;

            // Do realPosition.
            Font.Size.Deconstruct(out int x, out int y);
            RealPositionConsole.Position = (newPosition + new Point((x / 2), (y / 2))).PixelLocationToConsole(Font);
        }

        XnaInput.MouseState previousMouseState;

        private void UpdateMouse()
        {
            if (previousMouseState == null)
            {
                previousMouseState = XnaInput.Mouse.GetState();
                return;
            }

            XnaInput.MouseState mouseState = XnaInput.Mouse.GetState();
            
            if (previousMouseState.X == mouseState.X && previousMouseState.Y == mouseState.Y)
                return;

            previousMouseState = mouseState;

            ViewPositionConsole.Position = new Point(mouseState.X, mouseState.Y);

            RealPositionConsole.Position = ViewPositionConsole.Position.PixelLocationToConsole(Font);
        }
    }
}
