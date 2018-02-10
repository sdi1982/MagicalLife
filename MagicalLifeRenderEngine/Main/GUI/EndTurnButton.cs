﻿using MagicalLifeAPI.World;
using MagicalLifeRenderEngine.Main.GUI.Click;
using MagicalLifeRenderEngine.Util;
using MagicalLifeSettings.Storage;
using System;
using System.Drawing;

namespace MagicalLifeRenderEngine.Main.GUI
{
    /// <summary>
    /// Knows how to draw the end turn button properly onto the screen.
    /// </summary>
    public static class EndTurnButtonGUI
    {
        private static readonly Bitmap State1;
        private static readonly Bitmap State2;

        private const int scaleSize = 20;
        private static Size ImageSize;
        private static Size screenSize;

        private static Point drawLocation;
        //private static Point drawLocation = new Point(0, 0);

        /// <summary>
        /// The ID of our click bounds.
        /// </summary>
        private static Guid boundsID;

        static EndTurnButtonGUI()
        {
            screenSize = MainWindow.Default.ScreenSize;
            ImageSize = new Size(screenSize.Width / scaleSize, screenSize.Height / scaleSize);
            EndTurnButtonGUI.drawLocation = new Point(screenSize.Width - ImageSize.Width, screenSize.Height - (ImageSize.Height + 35));

            Bitmap s1 = TextureRegister.GetTexture("EndTurnButtonState1.png");
            Bitmap s2 = TextureRegister.GetTexture("EndTurnButtonState2.png");

            EndTurnButtonGUI.State1 = new Bitmap(s1, ImageSize);
            EndTurnButtonGUI.State2 = new Bitmap(s2, ImageSize);

            ClickBounds bounds = new ClickBounds(new Rectangle(drawLocation, ImageSize), int.MaxValue);
            bounds.Clicked += Bounds_Clicked;
            boundsID = bounds.ID;
            ClickDistributor.AddClickBounds(bounds);
        }

        private static void Bounds_Clicked(object sender, System.Windows.Forms.MouseEventArgs e)
        {
            World.EndTurn();
        }

        /// <summary>
        /// Draws the end turn button onto the screen properly.
        /// </summary>
        /// <param name="screen"></param>
        public static void Draw(ref Bitmap screen)
        {
            Bitmap currentTexture;

            if (World.IsPlayersTurn)
            {
                currentTexture = State1;
            }
            else
            {
                currentTexture = State2;
            }
            GraphicalUtils.DrawBitmapOnBitmap(currentTexture, screen, drawLocation);
        }
    }
}