﻿using MagicalLifeAPI.Filing.Logging;
using MagicalLifeGUIWindows.GUI.MainMenu;
using MagicalLifeGUIWindows.GUI.Reusable;
using MagicalLifeGUIWindows.Rendering.Text;
using Microsoft.Xna.Framework;
using Microsoft.Xna.Framework.Graphics;
using System;
using System.Collections.Generic;
using System.Diagnostics;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using static MagicalLifeGUIWindows.Rendering.Text.SimpleTextRenderer;

namespace MagicalLifeGUIWindows.Rendering.GUI
{
    /// <summary>
    /// Knows how to render various GUI components.
    /// </summary>
    public static class GUIRenderer
    {
        //public static void DrawInputBox(InputBox textbox, ref SpriteBatch spBatch)
        //{
        //    spBatch.Draw(textbox.Image, textbox.DrawingBounds, RenderingPipe.colorMask);
        //    DrawString(textbox.Font, textbox.Text, textbox.DrawingBounds, Alignment.Right, RenderingPipe.colorMask, ref spBatch);

        //    Rectangle carrotLocation = textbox.DrawingBounds;
        //    carrotLocation.X += (InputBox.CarrotSize * textbox.CarrotPosition);

        //    spBatch.Draw(textbox.CarrotTexture, carrotLocation, RenderingPipe.colorMask);
        //}

        public static void DrawInputBoxInContainer(InputBox textbox, ref SpriteBatch spBatch, GUIContainer container)
        {
            Rectangle location;
            int x = textbox.DrawingBounds.X + container.DrawingBounds.X;
            int y = textbox.DrawingBounds.Y + container.DrawingBounds.Y;
            location = new Rectangle(x, y, textbox.DrawingBounds.Width, textbox.DrawingBounds.Height);
            spBatch.Draw(textbox.Image, location, RenderingPipe.colorMask);
            DrawString(textbox.Font, textbox.Text, location, Alignment.Left, RenderingPipe.colorMask, ref spBatch);

            Rectangle carrotLocation = CalculateCarrotBounds(textbox, container);

            spBatch.Draw(textbox.CarrotTexture, carrotLocation, RenderingPipe.colorMask);
        }

        private static Rectangle CalculateCarrotBounds(InputBox textbox, GUIContainer container)
        {
            //if (textbox.Text != string.Empty)
            //{
            //    Debugger.Break();
            //}
            Vector2 size = textbox.Font.MeasureString(textbox.Text);
            Vector2 pos = new Vector2(textbox.DrawingBounds.Center.X, textbox.DrawingBounds.Center.Y);
            Vector2 origin = size * 0.5f;

#pragma warning disable RCS1096 // Use bitwise operation instead of calling 'HasFlag'.
            if (textbox.TextAlignment.HasFlag(Alignment.Left))
            {
                origin.X += (textbox.DrawingBounds.Width / 2) - (size.X / 2);
            }

            if (textbox.TextAlignment.HasFlag(Alignment.Right))
            {
                origin.X -= (textbox.DrawingBounds.Width / 2) - (size.X / 2);
            }

            if (textbox.TextAlignment.HasFlag(Alignment.Top))
            {
                origin.Y += (textbox.DrawingBounds.Height / 2) - (size.Y / 2);
            }

            if (textbox.TextAlignment.HasFlag(Alignment.Bottom))
            {
                origin.Y -= (textbox.DrawingBounds.Height / 2) - (size.Y / 2);
            }

            string TextBeforeCarrot = textbox.Text.Substring(0, textbox.CarrotPosition);
            int XPos = (int)Math.Round(origin.X + textbox.DrawingBounds.X + textbox.Font.MeasureString(TextBeforeCarrot).X) + container.DrawingBounds.X;
            int YPos = (int)Math.Round(origin.Y + textbox.DrawingBounds.Y) + container.DrawingBounds.Y;
            //int XPos = (int)Math.Round(pos.X + textbox.Font.MeasureString(TextBeforeCarrot).X);
            //int YPos = (int)Math.Round(pos.Y + textbox.Font.MeasureString(TextBeforeCarrot).Y);

            return new Rectangle(XPos, YPos, textbox.CarrotWidth, textbox.CarrotHeight);
        }

        public static void DrawButtonInContainer(MonoButton button, ref SpriteBatch spBatch, GUIContainer container)
        {
            Rectangle location;
            int x = button.DrawingBounds.X + container.DrawingBounds.X;
            int y = button.DrawingBounds.Y + container.DrawingBounds.Y;
            location = new Rectangle(x, y, button.DrawingBounds.Width, button.DrawingBounds.Height);
            spBatch.Draw(button.Image, location, RenderingPipe.colorMask);
            SimpleTextRenderer.DrawString(button.Font, button.Text, location, Alignment.Center, RenderingPipe.colorMask, ref spBatch);
        }

        //public static void DrawButton(MonoButton button, ref SpriteBatch spBatch)
        //{
        //    if (button.Visible)
        //    {
        //        spBatch.Draw(button.Image, button.DrawingBounds, RenderingPipe.colorMask);
        //        SimpleTextRenderer.DrawString(MainMenuLayout.MainMenuFont, button.Text, button.DrawingBounds, Alignment.Center, RenderingPipe.colorMask, ref spBatch);
        //    }
        //}
    }
}
