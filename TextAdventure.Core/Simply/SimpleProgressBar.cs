﻿using SadConsole;
using System;
using System.Collections.Generic;
using System.Runtime.Serialization;
using System.Text;

namespace TextAdventure.Core.Simply
{
    /// <summary>
    /// Simplified version of <see cref="SadConsole.Controls.ProgressBar"/>, made to work as a <see cref="SadConsole.CellSurface"/>
    /// </summary>
    public class SimpleProgressBar : SadConsole.CellSurface
    {
        /// <summary>
        /// Called when the <see cref="Progress"/> property value changes.
        /// </summary>
        public event EventHandler ProgressChanged;

        /// <summary>
        /// The progress bar fill value. Between 0f and 1f.
        /// </summary>
        [DataMember]
        protected float progressValue;

        /// <summary>
        /// The size of the bar.
        /// </summary>
        [DataMember]
        protected int controlSize;

        /// <summary>
        /// The size of the bar currently filled based on the <see cref="Progress"/> property.
        /// </summary>
        [DataMember]
        public int fillSize;

        /// <summary>
        /// Flag to indicate this bar was created horizontal.
        /// </summary>
        [DataMember]
        protected bool isHorizontal;

        /// <summary>
        /// The alignment if the bar is horizontal.
        /// </summary>
        [DataMember]
        protected HorizontalAlignment horizontalAlignment;

        /// <summary>
        /// The alignment if the bar is vertical.
        /// </summary>
        [DataMember]
        protected VerticalAlignment verticalAlignment;

        /// <summary>
        /// The horizontal orientation used when <see cref="IsHorizontal"/> is set to true.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the value is set to either <see cref="HorizontalAlignment.Center"/> or <see cref="HorizontalAlignment.Stretch"/>.
        /// </exception>
        public HorizontalAlignment HorizontalAlignment
        {
            get => horizontalAlignment;
            set
            {
                if (value == HorizontalAlignment.Center || value == HorizontalAlignment.Stretch)
                    throw new InvalidOperationException("HorizontalAlignment.Center or HorizontalAlignment.Stretch is invalid for the progress bar control.");

                horizontalAlignment = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// The vertical orientation used when <see cref="IsHorizontal"/> is set to false.
        /// </summary>
        /// <exception cref="InvalidOperationException">
        /// Thrown when the value is set to either <see cref="VerticalAlignment.Center"/> or <see cref="VerticalAlignment.Stretch"/>.
        /// </exception>
        public VerticalAlignment VerticalAlignment
        {
            get => verticalAlignment;
            set
            {
                if (value == VerticalAlignment.Center || value == VerticalAlignment.Stretch)
                    throw new InvalidOperationException("VerticalAlignment.Center or VerticalAlignment.Stretch is invalid for the progress bar control.");

                verticalAlignment = value;
                IsDirty = true;
            }
        }

        /// <summary>
        /// When true, the progress bar uses the <see cref="HorizontalAlignment"/> property to determine the starting
        /// fill direction. When false, uses the <see cref="VerticalAlignment"/> property.
        /// </summary>
        public bool IsHorizontal
        {
            get => isHorizontal;
            set
            {
                isHorizontal = value;
                var temp = progressValue;
                progressValue = -1f;
                Progress = temp;
            }
        }

        /// <summary>
        /// Gets or sets the value of the scrollbar between the minimum and maximum values.
        /// </summary>
        public float Progress
        {
            get => progressValue;
            set
            {
                if (progressValue != value)
                {
                    progressValue = value;

                    if (progressValue < 0)
                        progressValue = 0;
                    else if (progressValue > 1)
                        progressValue = 1;

                    if (progressValue == 0)
                        fillSize = 0;
                    else if (progressValue == 1)
                        fillSize = controlSize;
                    else
                        fillSize = (int)(controlSize * progressValue);

                    this.IsDirty = true;

                    ProgressChanged?.Invoke(this, EventArgs.Empty);
                }
            }
        }

        /// <summary>
        /// Creates a new horizontal progress bar.
        /// </summary>
        /// <param name="width">Width of the control.</param>
        /// <param name="height">Height of the control.</param>
        /// <param name="horizontalAlignment">Sets the control to be horizontal, starting from the specified side. Center/Stretch is invalid.</param>
        /// <exception cref="InvalidOperationException">
        /// Thrown when <paramref name="horizontalAlignment"/> is set to either <see cref="HorizontalAlignment.Center"/> or <see cref="HorizontalAlignment.Stretch"/>.
        /// </exception>
        public SimpleProgressBar(int width, int height, HorizontalAlignment horizontalAlignment) : base(width, height)
        {
            if (horizontalAlignment == HorizontalAlignment.Center || horizontalAlignment == HorizontalAlignment.Stretch)
                throw new InvalidOperationException("HorizontalAlignment.Center or HorizontalAlignment.Stretch is invalid for the progress bar control.");

            this.horizontalAlignment = horizontalAlignment;
            isHorizontal = true;
            controlSize = width;
        }

        /// <summary>
        /// Creates a new vertical progress bar.
        /// </summary>
        /// <param name="width">Width of the control.</param>
        /// <param name="height">Height of the control.</param>
        /// <param name="verticalAlignment">Sets the control to be vertical, starting from the specified side. Center/Stretch is invalid.</param>
        /// <exception cref="InvalidOperationException">Thrown when <paramref name="verticalAlignment"/> is set to either <see cref="VerticalAlignment.Center"/> or <see cref="VerticalAlignment.Stretch"/>.</exception>
        public SimpleProgressBar(int width, int height, VerticalAlignment verticalAlignment) : base(width, height)
        {
            if (verticalAlignment == VerticalAlignment.Center || verticalAlignment == VerticalAlignment.Stretch)
                throw new InvalidOperationException("VerticalAlignment.Center or VerticalAlignment.Stretch is invalid for the progress bar control.");

            this.verticalAlignment = verticalAlignment;
            isHorizontal = false;
            controlSize = height;
        }

        [OnDeserialized]
        private void AfterDeserialized(StreamingContext context)
        {
            var temp = progressValue;
            progressValue = -1;
            Progress = temp;
            IsDirty = true;
        }
    }
}
