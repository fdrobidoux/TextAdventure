using System;
using System.Collections.Generic;
using System.Linq;
using SadConsole;
using SadConsole.Controls;
using Microsoft.Xna.Framework;
using SadConsole.Themes;

namespace TextAdventure.Core.Console
{
    public class TestHealthConsole : ControlsConsole
    {
        List<SadConsole.Controls.Button> IncrementButtons;
        List<SadConsole.Controls.Button> MinusButtons;

        ButtonTheme buttonTheme;

        public TestHealthConsole(int width, int height) : base(width, height)
        {
            IncrementButtons = new List<Button>() {
                new Button(7, 3) { Text = "+1%", Name = "PlusOnePercent" },
                new Button(8, 3) { Text = "+10%", Name = "PlusTenPercent" },
                new Button(8, 3) { Text = "+25%", Name = "PlusTwentyFivePercent" }
            };

            MinusButtons = new List<Button>() {
                new Button(7, 3) { Text = "-1%", Name = "MinusOnePercent" },
                new Button(8, 3) { Text = "-10%", Name = "MinusTenPercent" },
                new Button(8, 3) { Text = "-25%", Name = "MinusTwentyFivePercent" }
            };

            Button currentButton;
            Point daPoint = Point.Zero;
            List<Button>[] lsOfBtnListe = new[] { IncrementButtons, MinusButtons };
            int biggestHeightForRow = 0;
            int lastLineBiggestHeight = 0;
            List<Button> btnList;

            buttonTheme = new ButtonLinesTheme();

            for (var i = 0; i < lsOfBtnListe.Length; i++)
            {
                btnList = lsOfBtnListe[i];

                for (var j = 0; j < btnList.Count; j++)
                {
                    currentButton = btnList[j];

                    currentButton.Theme = buttonTheme;
                    currentButton.TextAlignment = HorizontalAlignment.Center;
                    
                    // Somehow, this works. I have no idea how, my hands wrote this, ask them !
                    currentButton.Position = new Point((currentButton.Width * j) + j, lastLineBiggestHeight * i);

                    // I don't care about good UX tbh, so align rows based on biggest height in last row.
                    biggestHeightForRow = Math.Max(biggestHeightForRow, currentButton.Height);

                    // Bind click event.
                    currentButton.Click += CurrentButton_Click;

                    this.Add(currentButton);
                }

                lastLineBiggestHeight = biggestHeightForRow;
            }
        }

        public event EventHandler<float> ClickAny;

        private void CurrentButton_Click(object sender, EventArgs e)
        {
            if (!(sender is Button btn)) return;

            float valeur = 0.0f;

            switch (btn.Name)
            {
                case "PlusOnePercent":
                    valeur = 0.01f;
                    break;
                case "PlusTenPercent":
                    valeur = 0.1f;
                    break;
                case "PlusTwentyFivePercent":
                    valeur = 0.25f;
                    break;
                case "MinusOnePercent":
                    valeur = -0.01f;
                    break;
                case "MinusTenPercent":
                    valeur = -0.1f;
                    break;
                case "MinusTwentyFivePercent":
                    valeur = -0.25f;
                    break;
                default:
                    throw new NotImplementedException("Btn n'est pas géré.");
            }

            ClickAny?.Invoke(btn, valeur);
        }
    }
}
