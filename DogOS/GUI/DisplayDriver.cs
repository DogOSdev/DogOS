using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.System.Graphics;
using System.Drawing;

namespace DogOS.GUI
{
    public class DisplayDriver
    {
        public Canvas screen;
        public int width, height = 0;

        #region Init

        public DisplayDriver()
        {
            screen = FullScreenCanvas.GetFullScreenCanvas();

            // For some reason, columns and rows are switched.
            // So store them as such.
            width = Convert.ToInt32(screen.Mode.Columns);
            height = Convert.ToInt32(screen.Mode.Rows);
        }

        public DisplayDriver(int width, int height)
        {
            // Store the width and height.
            this.width = width;
            this.height = height;

            // Create the screen with the new mode.
            screen = FullScreenCanvas.GetFullScreenCanvas(new Mode(width, height, ColorDepth.ColorDepth32));
        }

        #endregion;

        #region Drawing Functions

        public void Pixel(int x, int y, Color c)
        {
            var pen = new Pen(c);
            screen.DrawPoint(pen, x, y);
        }

        public void Clear()
        {
            Clear(Color.Black);
        }

        public void Clear(Color c)
        {
            screen.Clear(c);
        }

        public void Line(int x_start, int y_start, int x_end, int y_end, Color c)
        {
            var pen = new Pen(c);
            screen.DrawLine(pen, x_start, y_start, x_end, y_end);
        }

        public void Rectangle(int x_start, int y_start, int width, int height, Color c, bool filled = false)
        {
            var pen = new Pen(c);
            switch(filled)
            {
                case false:
                    screen.DrawRectangle(pen, x_start, y_start, width, height);
                    break;
                case true:
                    screen.DrawFilledRectangle(pen, x_start, y_start, width, height);
                    break;
            }
        }

        #endregion;
    }
}
