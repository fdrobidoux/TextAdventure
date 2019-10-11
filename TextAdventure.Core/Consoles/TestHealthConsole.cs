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

        public TestHealthConsole(int width, int height) : base(width, height)
        {
            IncrementButtons = new List<Button>() {
                new Button(7, 3) { Text = "+1%", TextAlignment = HorizontalAlignment.Center, Name = "Btn_PlusOnePercent" },
                new Button(8, 3) { Text = "+10%", TextAlignment = HorizontalAlignment.Center, Name = "Btn_PlusTenPercent" },
                new Button(8, 3) { Text = "+25%", TextAlignment = HorizontalAlignment.Center, Name = "Btn_PlusTwentyFivePercent" }
            };

            MinusButtons = new List<Button>() {
                new Button(7, 3) { Text = "-1%", TextAlignment = HorizontalAlignment.Center, Name = "Btn_MinusOnePercent" },
                new Button(8, 3) { Text = "-10%", TextAlignment = HorizontalAlignment.Center, Name = "Btn_MinusTenPercent" },
                new Button(8, 3) { Text = "-25%", TextAlignment = HorizontalAlignment.Center, Name = "Btn_MinusTwentyFivePercent" }
            };

            Button currentButton;
            Point daPoint = Point.Zero;
            List<Button>[] lsOfBtnListe = new[] { IncrementButtons, MinusButtons };
            int biggestHeightForRow = 0;
            int lastLineBiggestHeight = 0;

            for (var i = 0; i < lsOfBtnListe.Length; i++)
            {
                List<Button> btnList = lsOfBtnListe[i];

                for (var j = 0; j < btnList.Count; j++)
                {
                    currentButton = btnList[j];

                    currentButton.Theme = new ButtonLinesTheme();

                    currentButton.Position = new Point((currentButton.Width * j) + j, lastLineBiggestHeight * i);

                    biggestHeightForRow = Math.Max(biggestHeightForRow, currentButton.Height);

                    this.Add(currentButton);
                    currentButton.Click += CurrentButton_Click;
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
                case "Btn_PlusOnePercent":
                    valeur = 0.01f;
                    break;
                case "Btn_PlusTenPercent":
                    valeur = 0.1f;
                    break;
                case "Btn_PlusTwentyFivePercent":
                    valeur = 0.25f;
                    break;
                case "Btn_MinusOnePercent":
                    valeur = -0.01f;
                    break;
                case "Btn_MinusTenPercent":
                    valeur = -0.1f;
                    break;
                case "Btn_MinusTwentyFivePercent":
                    valeur = -0.25f;
                    break;
                default:
                    throw new NotImplementedException("Btn n'est pas géré.");
            }

            ClickAny?.Invoke(btn, valeur);
        }
    }
}
