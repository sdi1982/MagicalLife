﻿using MagicalLifeAPI.Universal;
using System;
using System.Drawing;
using System.Windows.Forms;

namespace MagicalLifeRenderEngine.Main.GUI.Click
{
    /// <summary>
    /// Holds information about where a click is clicking within bounds, as well as priority and a event to subscribe to.
    /// </summary>
    public class ClickBounds : Unique
    {
        /// <summary>
        /// The range of this <see cref="ClickBounds"/> obejct.
        /// </summary>
        public Rectangle Bounds { get; set; }

        /// <summary>
        /// The priority of this click bounds.
        /// The higher the value, the higher the priority.
        /// </summary>
        public int Priority { get; set; }

        /// <summary>
        /// Constructs a new instance of the <see cref="ClickBounds"/> class.
        /// </summary>
        /// <param name="startingLocation">The point where this click bounds begins.</param>
        /// <param name="size">The size of this click bounds.</param>
        /// <param name="priority">The priority of this click bounds. Must be equal to or greater than 0, unless this clickbounds ALWAYS has priority over other click bounds.</param>
        public ClickBounds(Rectangle bounds, int priority)
        {
            this.Bounds = bounds;
            this.Priority = priority;
        }

        /// <summary>
        /// This event is raised whenever this clickbounds is clicked on, and given priority.
        /// </summary>
        public event EventHandler<MouseEventArgs> Clicked;

        /// <summary>
        /// Raises the Clicked event.
        /// </summary>
        /// <param name="e"></param>
        public virtual void ClickMe(MouseEventArgs e)
        {
            EventHandler<MouseEventArgs> handler = Clicked;
            if (handler != null)
            {
                handler(this, e);
            }
        }
    }
}