using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using SadConsole;
using SadConsole.Controls;

namespace TextAdventure.Core.UI
{
    public partial class HealthProgressBar : SadConsole.Controls.ProgressBar
    {
        /// <summary>
        /// Text value within the health progress bar.
        /// </summary>
        [DataMember]
        protected string textValue;

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
            this.Progress = 1.0f;
            this.bindFreshDmgOnProgressChanged();
            this.createStartDropTimer();
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

#endregion
    }
}
