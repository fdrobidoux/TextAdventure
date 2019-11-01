using System;
using Microsoft.Xna.Framework;
using SadConsole;
using SadConsole.Controls;
using SadConsole.Themes;
using TextAdventure.Core.UI;

namespace TextAdventure.Core.Themes
{
    public partial class HealthProgressBarTheme : SadConsole.Themes.ProgressBarTheme
    {
        public Cell FreshDamage;

        public Rectangle dmgRect;
        private PreviousStateEncapsulator previousState;

        public HealthProgressBarTheme() : base()
        {

        }

        public override void UpdateAndDraw(ControlBase control, TimeSpan time)
        {
            bool initialDirtiness = control.IsDirty;

            base.UpdateAndDraw(control, time);

            if ((control is HealthProgressBar hpBar))
            {
                this.UpdateAndDraw_FreshDamage(hpBar, time, initialDirtiness);
                //this.UpdateAndDraw_Text()
            }
        }

        public void UpdateAndDraw_FreshDamage(HealthProgressBar hpBar, TimeSpan time, bool InitialDirtiness)
        {
            // Add to the Initial dirtiness, checking if there really was an initial dirtiness.
            InitialDirtiness &= hpBar.freshDmgFillSize > 0;

            if (!previousState.Equals(hpBar))
            {
                previousState = new PreviousStateEncapsulator(hpBar);
                dmgRect = new Rectangle(hpBar.fillSize, 0, hpBar.freshDmgFillSize, hpBar.Height);
                hpBar.IsDirty = true;
            }

            if (InitialDirtiness || hpBar.IsDirty)
            {
                hpBar.Surface.Fill(dmgRect, FreshDamage.Foreground, FreshDamage.Background, 178, 0);
            }
        }

        public override void Attached(ControlBase control) => base.Attached(control);

        public override ThemeBase Clone() => base.Clone();

        public override void RefreshTheme(Colors themeColors)
        {
            base.RefreshTheme(themeColors);

            FreshDamage = themeColors.Appearance_ControlDisabled;
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

            public PreviousStateEncapsulator(HealthProgressBar hpb) : this(hpb.fillSize, hpb.freshDmgFillSize) { }

            public bool Equals(HealthProgressBar other)
            {
                return (other.fillSize == this.fillSize) 
                    && (other.freshDmgFillSize == this.freshDmgFillSize);
            }

        }
    }
}
