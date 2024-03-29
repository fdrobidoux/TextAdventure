﻿using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Controls;
using TextAdventure.Core.Consoles.Inventory;
using TextAdventure.Core.Themes;
using TextAdventure.Core.UI;

namespace TextAdventure.Core.Consoles
{
    public class HUDConsole : SadConsole.ControlsConsole
    {
        public Label HpLabel { get; private set; }
        public HealthProgressBar HpProgressBar { get; private set; }

        public Label ManaLabel { get; private set; }
        public HealthProgressBar ManaProgressBar { get; private set; }

        public Button InventoryBtn { get; private set; }

        public HUDConsole(int width, int height) : base(width, height)
        {
            this.Theme.ControlsConsoleTheme.FillStyle.Background = Color.DarkGray;

            // ########
            // # Health
            // ########

            Add(HpLabel = new Label("Health: ")
            {
                Position = new Point(1, 1), 
                CanFocus = false
            });

            Add(HpProgressBar = new HealthProgressBar(width / 4, 1, HorizontalAlignment.Left)
            {
                Position = (HpLabel.Position + new Point(HpLabel.Width, 0)),
                CanFocus = false,
                Theme = new HealthProgressBarTheme()
            });

            var hpColors = Theme.Colors.Clone();
            // Normal
            hpColors.Text = Color.Red;
            hpColors.ControlBack = Color.Salmon;
            // Damage
            hpColors.TextLight = Color.DarkRed;
            hpColors.ControlBackDark = Color.Black;
            
            hpColors.RebuildAppearances();
            HpProgressBar.Theme.RefreshTheme(hpColors);

            // ######
            // # Mana
            // ######
            Add(ManaLabel = new Label("Mana :")
            {
                Position = (HpLabel.Position + new Point(0, 1)),
                CanFocus = false
            });
            Add(ManaProgressBar = new HealthProgressBar(width / 4, 1, HorizontalAlignment.Left)
            {
                Position = (ManaLabel.Position + new Point(HpLabel.Width, 0)),
                CanFocus = false,
                Theme = new HealthProgressBarTheme()
            });

            var manaColors = Theme.Colors.Clone();
            // Normal
            manaColors.Text = Color.DarkBlue;
            manaColors.ControlBack = Color.DarkSlateGray;
            // Damage
            manaColors.TextLight = Color.GhostWhite;
            manaColors.ControlBackDark = Color.Navy;
            
            manaColors.RebuildAppearances();
            ManaProgressBar.Theme.RefreshTheme(manaColors);

            // # Menus
            Add(InventoryBtn = new Button(13, 3)
            {
                Position = new Point(width / 2, 0),
                Text = "Inventory",
                TextAlignment = HorizontalAlignment.Center,
            });
        }
    }
}
