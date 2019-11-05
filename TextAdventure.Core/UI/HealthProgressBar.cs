using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using SadConsole;
using SadConsole.Controls;
using TextAdventure.Core.Themes;

namespace TextAdventure.Core.UI
{
    public partial class HealthProgressBar : SadConsole.Controls.ProgressBar
    {
        public HealthProgressBar(int width, int height, HorizontalAlignment horizontalAlignment) : base(width, height, horizontalAlignment)
        {
            this.beforeConstruct();
            // TODO: Constructor for Horizontal-aligned HealthProgressBar
        }

        public HealthProgressBar(int width, int height, VerticalAlignment verticalAlignment) : base(width, height, verticalAlignment)
        {
            this.beforeConstruct();
            // TODO: Constructor for Vertical-aligned HealthProgressBar
        }

        private void beforeConstruct()
        {
            this.Theme = new HealthProgressBarTheme();
            this.Progress = 1.0f;
            this.bindFreshDmgOnProgressChanged();
            //this.createStartDropTimer();
        }

#region "ProgressBar Overrides"

        public override void Update(TimeSpan time)
        {
            base.Update(time);
            this.updateFreshDamageBar(time);
        }

        protected override void OnParentChanged()
        {
            base.OnParentChanged();
        }

        protected override void OnPositionChanged()
        {
            base.OnPositionChanged();
        }

        protected override void OnStateChanged(ControlStates oldState, ControlStates newState)
        {
            base.OnStateChanged(oldState, newState);
        }

        protected override void OnThemeChanged()
        {
            base.OnThemeChanged();
        }

        public void TestHPConsole_ClickAny(object sender, float e)
        {
            this.Progress = MathHelper.Clamp(this.Progress + e, 0.0f, 1.0f);
        }

        #endregion
    }
}
