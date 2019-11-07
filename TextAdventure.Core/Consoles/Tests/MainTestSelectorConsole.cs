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
        private bool _testGoingOn = false;
        private Action _testDisposerSub;

        public SadConsole.ControlsConsole SelectionConsole;

        public MainTestSelectorConsole() : base(Global.CurrentScreen.Width, Global.CurrentScreen.Height)
        {
            UseKeyboard = true;

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
                    {
                        Children.Add(cli);
                    }

                    //return testHPConsoles.AsEnumerable();
                },
                disposer: () =>
                {
                    Children.RemoveAll(out IEnumerable<HUDConsole> removedHUD);
                    removedHUD.First().RemoveAll();

                    Children.RemoveAll(out IEnumerable<TestHealthConsole> removedTestHPConsoles);
                    foreach (TestHealthConsole testHpCli in removedTestHPConsoles)
                        testHpCli.RemoveAll();
                }));

            // Test pixel offset.
            SelectionConsole.Add(MakeTestButtonFor<TestPixelOffsetConsole>("Pixel Offset", "BtnTestPixelOffsetConsole",
                creator: () =>
                {
                    var myTestPixelOffsetConsole = new TestPixelOffsetConsole(10, 5);
                    Children.Add(myTestPixelOffsetConsole);
                    //return new List<TestPixelOffsetConsole>() { myTestPixelOffsetConsole };
                },
                disposer: () =>
                {
                    Children.RemoveAll();
                }));

            SelectionConsole.Add(MakeTestButtonFor<TestAnimatedEntitiesConsole>("", "",
                creator: () =>
                {

                },
                disposer: () =>
                {

                }));
        }

        private Button MakeTestButtonFor<ConsoleType>(string text, string name, Action creator, Action disposer) where ConsoleType : SadConsole.Console
        {
            var btn = new Button(text.Length + 2, 1)
            {
                Text = "Health Bars",
                Name = "BtnTestHealth",
                Theme = new SadConsole.Themes.Button3dTheme()
            };

            btn.Click += (s, e) =>
            {
                /*IEnumerable<ConsoleType> consolesCreated = */creator.Invoke();
                SelectionConsole.IsVisible = false;
                _testGoingOn = true;
            };
            _testDisposerSub = disposer;

            return btn;
        }

        private void SetUpHealthBarTest()
        {
            var btn = new SadConsole.Controls.Button(10, 1)
            {
                Text = "Health Bars",
                Name = "BtnTestHealth",
                Theme = new SadConsole.Themes.Button3dTheme()
            };
            SelectionConsole.Add(btn);
        }

        public override bool ProcessKeyboard(Keyboard info)
        {
            if (info.IsKeyDown(Microsoft.Xna.Framework.Input.Keys.Escape))
            {
                if (_testGoingOn)
                {
                    _testDisposerSub.Invoke();
                    SelectionConsole.IsVisible = true;
                    _testGoingOn = false;
                }

            }
            return base.ProcessKeyboard(info);
        }

        public override void Draw(TimeSpan timeElapsed)
        {
            base.Draw(timeElapsed);
        }

        public override void Update(TimeSpan timeElapsed)
        {
            base.Update(timeElapsed);
        }
    }
}
