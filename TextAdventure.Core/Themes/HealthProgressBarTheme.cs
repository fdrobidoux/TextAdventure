using System;
using System.Collections.Generic;
using System.Linq;
using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Controls;
using SadConsole.Effects;
using SadConsole.Themes;
using TextAdventure.Core.UI;

namespace TextAdventure.Core.Themes
{
    public partial class HealthProgressBarTheme : SadConsole.Themes.ProgressBarTheme
    {
        public Cell FreshDamage;

        public Rectangle dmgRect;
        private PreviousStateEncapsulator previousState;
        private float previousProgress;

        public SadConsole.Effects.Fade FreshDamageEffect;

        public HealthProgressBarTheme() : base()
        {
            FreshDamageEffect = new SadConsole.Effects.Fade()
            {
                DestinationForeground = new ColorGradient(FreshDamage.Foreground),
                DestinationBackground = new ColorGradient(FreshDamage.Background),
                FadeBackground = true,
                FadeForeground = true,
                UseCellForeground = true,
                UseCellDestinationReverse = true,
                FadeDuration = 1f,
                RemoveOnFinished = true,
            };
        }

        public override void UpdateAndDraw(ControlBase control, TimeSpan time)
        {
            base.UpdateAndDraw(control, time);

            if (control.Surface.Effects.Count != 0)
            {
                control.Surface.Effects.UpdateEffects(time.TotalSeconds);
                control.IsDirty = true;
            }

            if ((control is HealthProgressBar hpBar))
            {
                //this.UpdateAndDraw_FreshDamage(hpBar, time);
                //this.UpdateAndDraw_Text()
            }
        }

        public void UpdateAndDraw_FreshDamage(HealthProgressBar hpBar, TimeSpan time)
        {
            // Add to the Initial dirtiness, checking if there really was an initial dirtiness.

            if (previousState.fillSize != hpBar.fillSize)
            {
                previousState = new PreviousStateEncapsulator(hpBar);
                dmgRect = new Rectangle(hpBar.fillSize, 0, hpBar.FreshDmgFillSize, hpBar.Height);
                hpBar.Surface.SetEffect(hpBar.Surface.GetCells(dmgRect), FreshDamageEffect);
                hpBar.IsDirty = true;
            }
        }

        public override void Attached(ControlBase control)
        {
            base.Attached(control);

            if (!(control is HealthProgressBar hpBar))
                return;

            dmgRect = new Rectangle(hpBar.fillSize, 0, hpBar.FreshDmgFillSize, hpBar.Height);
            previousState = new PreviousStateEncapsulator(hpBar);

            hpBar.ProgressChanged += HpBar_ProgressChanged;
        }

        private List<Cell> progressingCells = new List<Cell>();
        private List<ICellEffect> progressingEffects = new List<ICellEffect>();

        private void HpBar_ProgressChanged(object sender, EventArgs e)
        {
            /* Plans for this function :
             * - If fill size hasn't changed but progress has changed and is negative compared to last state, put a fade effect on last fill cell (from left to right)
             * - Translate effects to an offset so that we keep the state of an effect happening.
             */


            if (!(sender is HealthProgressBar hpBar)) 
                return;

            // Set fresh damage fill size based on how many effects are still active.
            if (progressingEffects.Count > 0)
            {
                var effectsDone = progressingEffects.Where(x => x.IsFinished);
                hpBar.FreshDmgFillSize -= effectsDone.Count();
            }

            // Obtain all the non-finished effects and translate them
            // say previous fill size = 0
            int offset = previousState.freshDmgFillSize - 






            dmgRect = new Rectangle(hpBar.fillSize, 0, hpBar.FreshDmgFillSize, hpBar.Height);

            progressingCells = hpBar.Surface.GetCells(dmgRect).ToList();
            progressingEffects.Clear();
            int allCellsCount = progressingCells.Count();

            //foreach (var effect in hpBar.Surface.Effects.GetEffects().OfType<Fade>())
            //{
            //    effect.Restart();
            //}

            for (int i = 0; i < allCellsCount; i++)
            {
                Cell cell = progressingCells.ElementAt(i);

                // Fetch finished effects

                // Make new effect.
                Fade copyFade = (Fade)FreshDamageEffect.Clone();
                copyFade.StartDelay = 0.2f * (allCellsCount - i);
                hpBar.Surface.SetEffect(cell, copyFade);
                progressingEffects.Add(copyFade);
            }

            previousState = new PreviousStateEncapsulator(hpBar);
        }

        public override ThemeBase Clone() => base.Clone();

        public override void RefreshTheme(Colors themeColors)
        {
            base.RefreshTheme(themeColors);

            FreshDamage = themeColors.Appearance_ControlDisabled;
            
            if (FreshDamageEffect != null)
            {
                FreshDamageEffect.DestinationForeground = new ColorGradient(FreshDamage.Foreground);
                FreshDamageEffect.DestinationBackground = new ColorGradient(FreshDamage.Background);
            }
        }

        private struct PreviousStateEncapsulator: IEquatable<HealthProgressBar>
        {
            public int freshDmgFillSize;
            public int fillSize;
            
            public PreviousStateEncapsulator(int fillSize, int freshDmgFillSize)
            {
                this.fillSize = fillSize;
                this.freshDmgFillSize = freshDmgFillSize;
            }

            public PreviousStateEncapsulator(HealthProgressBar hpb) : this(hpb.fillSize, hpb.FreshDmgFillSize) { }

            public bool Equals(HealthProgressBar other)
            {
                return (other.fillSize == this.fillSize) 
                    && (other.FreshDmgFillSize == this.freshDmgFillSize);
            }

        }
    }
}
