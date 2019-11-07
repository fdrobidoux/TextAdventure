using System;
using System.Collections.Generic;
using Microsoft.Xna.Framework;
using System.Linq;
using SadConsole;
using SadConsole.Controls;
using TextAdventure.Core.Consoles;
using SadConsole.Input;
using TextAdventure.Core.Consoles.Inventory;

namespace TextAdventure.Core.Consoles.Tests
{
    public class MainTestSelectorConsole : SadConsole.Console
    {
        private bool _isTestGoingOn = false;
        private Action _disposeur;

        public SadConsole.ControlsConsole SelectionConsole;

        public MainTestSelectorConsole() : base(Global.CurrentScreen.Width, Global.CurrentScreen.Height)
        {
            UseKeyboard = true;
            Cursor.Position = Point.Zero;

            SelectionConsole = new ControlsConsole(Global.CurrentScreen.Width, 6);

            // Test health and mana progress bars.
            SelectionConsole.Add(MakeTestButtonFor<TestHealthConsole>("Health bars", "BtnTestHealth",
                creator: () =>
                {
                    HUDConsole myHUDConsole;
                    InventoryConsole myInventoryConsole;

                    Children.Add(myHUDConsole = new HUDConsole(Global.CurrentScreen.Width, 4));
                    Children.Add(myInventoryConsole = new InventoryConsole()
                    {
                        Position = new Point(0, 8),
                        IsVisible = false
                    });
                    myHUDConsole.InventoryBtn.Click += (s, e) => myInventoryConsole.IsVisible = !myInventoryConsole.IsVisible;

                    TestHealthConsole[] testHPConsoles = new[] {
                        new TestHealthConsole(80, 20, myHUDConsole.HpProgressBar) { Position = new Point(0, 5) },
                        new TestHealthConsole(80, 20, myHUDConsole.ManaProgressBar) { Position = new Point(30, 5) }
                    };

                    foreach (TestHealthConsole cli in testHPConsoles)
                        Children.Add(cli);
                },
                disposer: () =>
                {
                    Children.RemoveAll(out IEnumerable<HUDConsole> removedHUD);
                    removedHUD.First().Clear();

                    Children.RemoveAll(out IEnumerable<TestHealthConsole> removedTestHPConsoles);
                    foreach (TestHealthConsole testHpCli in removedTestHPConsoles)
                        testHpCli.Clear();
                }));

            // Test pixel offset.
            SelectionConsole.Add(MakeTestButtonFor<TestPixelOffsetConsole>("Pixel Offset", "BtnTestPixelOffsetConsole",
                creator: () =>
                {
                    Children.Add(new TestPixelOffsetConsole(10, 5));
                },
                disposer: () =>
                {
                    Children.RemoveAll(out IEnumerable<TestPixelOffsetConsole> removedTest);
                    foreach (var child in removedTest)
                        child.Children.Clear();
                }));

            // Test animated entities.
            SelectionConsole.Add(MakeTestButtonFor<TestAnimatedEntitiesConsole>("Animated Entities", "BtnTestAnimatedEntities",
                creator: () =>
                {
                    Children.Add(new TestAnimatedEntitiesConsole());
                },
                disposer: () =>
                {
                    Children.RemoveAll(out IEnumerable<TestAnimatedEntitiesConsole> removedTest);
                    foreach (var child in removedTest)
                        child.Children.Clear();
                }));

            Children.Add(SelectionConsole);
        }

        private Button MakeTestButtonFor<ConsoleType>(string text, string name, Action creator, Action disposer) where ConsoleType : SadConsole.Console
        {
            // Create btn
            var btn = new Button(text.Length + 2, 1)
            {
                Text = text,
                Name = name,
                Theme = new SadConsole.Themes.Button3dTheme()
            };
            
            // Calc position
            Point calcPos = new Point(btn.Width + 2, 0);
            if ((Cursor.Position + calcPos).X > Global.CurrentScreen.Width)
            {
                Cursor.Position = new Point(0, Cursor.Position.Y + 1);
            }
            btn.Position = Cursor.Position;
            Cursor.Position = Cursor.Position + calcPos;

            // Add to SelectionConsole
            SelectionConsole.Add(btn);

            btn.Click += (s, e) =>
            {
                DeactivateCurrentTest();
                creator.Invoke();
                SelectionConsole.IsVisible = false;
                _isTestGoingOn = true;
            };
            _disposeur = disposer;

            return btn;
        }

        public override void Update(TimeSpan timeElapsed)
        {
            base.Update(timeElapsed);

            if (Global.KeyboardState.IsKeyPressed(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                DeactivateCurrentTest();
            }
        }

        public override bool ProcessKeyboard(Keyboard info)
        {
            if (info.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                DeactivateCurrentTest();
            }

            return base.ProcessKeyboard(info);
        }

        private void DeactivateCurrentTest()
        {
            if (_isTestGoingOn)
            {
                _disposeur?.Invoke();
                SelectionConsole.IsVisible = true;
                _isTestGoingOn = false;
            }
        }
    }
}
