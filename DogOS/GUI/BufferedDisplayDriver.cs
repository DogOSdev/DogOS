using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.System.Graphics;
using System.Drawing;

namespace DogOS.GUI
{
    public class BufferedDisplayDriver : DisplayDriver
    {
        private Color[] buffer;
        private Color[] old_buffer;

        public BufferedDisplayDriver() : base()
        {
            buffer = new Color[width*height];
            old_buffer = new Color[width*height];
        }

        public BufferedDisplayDriver(int width, int height) : base(width, height)
        {
            buffer = new Color[width*height];
            old_buffer = new Color[width*height];
        }

        public void DrawBuffer()
        {
            Pen pen = new Pen(Color.Black);

            for (int y = 0; y < height; y++)
            {
                for (int x = 0; x < width; x++)
                {
                    // If color has been changed in buffer, redraw.
                    if(buffer[width * y + x] != old_buffer[width * y + x])
                    {
                        pen.Color = buffer[width * y + x];
                        screen.DrawPoint(pen, x, y);
                    }
                }
            }

            // If buffer has been changed, replace old buffer
            if(buffer != old_buffer)
            {
                old_buffer = buffer;
            }
        }

        public new void Clear()
        {
            Clear(Color.Black);
        }

        public new void Clear(Color c)
        {
            Rectangle(0, 0, width, height, c, true);
        }

        public new void Pixel(int x, int y, Color c)
        {
            CheckValidCoords(x, y);

            buffer[width*y + x] = c;
        }

        // Based on CosmosOS method
        // https://github.com/CosmosOS/Cosmos/blob/e7aa2f45a5349f874b11689458e52c9a17dc7c08/source/Cosmos.System2/Graphics/Canvas.cs#L260
        public new void Line(int x_start, int y_start, int x_end, int y_end, Color c)
        {
            TrimLine(ref x_start, ref y_start, ref x_end, ref y_end);

            int dx = x_end - x_start;
            int dy = y_end - y_start;

            if(dy == 0)
            {
                HorizontalLine(dx, x_start, y_start, c);
                return;
            }

            if(dx == 0)
            {
                VerticalLine(dx, x_start, y_start, c);
                return;
            }

            DiagonalLine(dx, dy, x_start, y_end, c);
        }

        public new void Rectangle(int x_start, int y_start, int width, int height, Color c, bool filled = false)
        {
            switch (filled)
            {
                case false:
                    // point a - point b
                    Line(x_start, y_start, x_start + width, y_start, c);

                    // point a - point c
                    Line(x_start, y_start, x_start, y_start + height, c);

                    // point b - point d
                    Line(x_start + width, y_start, x_start + width, y_start + height, c);

                    // point c - point d
                    Line(x_start, y_start + height, x_start + width, y_start + height, c);
                    break;

                case true:
                    for (int y = y_start; y < y_start + height; y++)
                    {
                        Line(x_start, y, x_start + width - 1, y, c);
                    }
                    break;
            }
        }

        #region Private Drawing Methods

        private void CheckValidCoords(int x, int y)
        {
            if(x < 0 || x >= width)
            {
                throw new ArgumentOutOfRangeException($"x ({x}) is not between 0 and {width}");
            }

            if(y < 0 || y >= height)
            {
                throw new ArgumentOutOfRangeException($"y ({y}) is not between 0 and {height}");
            }
        }

        private void HorizontalLine(int amount, int x_start, int y_start, Color c)
        {
            for (int i = 0; i < amount; i++)
            {
                Pixel(x_start + i, y_start, c);
            }
        }

        private void VerticalLine(int amount, int x_start, int y_start, Color c)
        {
            for (int i = 0; i < amount; i++)
            {
                Pixel(x_start, y_start + i, c);
            }
        }
        
        // Based on CosmosOS method
        // https://github.com/CosmosOS/Cosmos/blob/e7aa2f45a5349f874b11689458e52c9a17dc7c08/source/Cosmos.System2/Graphics/Canvas.cs#L201
        private void DiagonalLine(int x_start, int y_start, int x1, int y1, Color c)
        {
            int dxabs = Math.Abs(x_start);
            int dyabs = Math.Abs(y_start);
            int sdx = Math.Sign(x_start);
            int sdy = Math.Sign(y_start);
            int x = dyabs >> 1;
            int y = dxabs >> 1;
            int px = x1;
            int py = y1;

            if (dxabs >= dyabs) /* the line is more horizontal than vertical */
            {
                for (int i = 0; i < dxabs; i++)
                {
                    y += dyabs;
                    if (y >= dxabs)
                    {
                        y -= dxabs;
                        py += sdy;
                    }
                    px += sdx;
                    Pixel(px, py, c);
                }
            }
            else /* the line is more vertical than horizontal */
            {
                for (int i = 0; i < dyabs; i++)
                {
                    x += dxabs;
                    if (x >= dyabs)
                    {
                        x -= dyabs;
                        px += sdx;
                    }
                    py += sdy;
                    Pixel(px, py, c);
                }
            }
        }
        
        // From the CosmosOS method
        // https://github.com/CosmosOS/Cosmos/blob/e7aa2f45a5349f874b11689458e52c9a17dc7c08/source/Cosmos.System2/Graphics/Canvas.cs#L1104
        private void TrimLine(ref int x1, ref int y1, ref int x2, ref int y2)
        {
            // in case of vertical lines, no need to perform complex operations
            if (x1 == x2)
            {
                x1 = Math.Min(width - 1, Math.Max(0, x1));
                x2 = x1;
                y1 = Math.Min(height - 1, Math.Max(0, y1));
                y2 = Math.Min(height - 1, Math.Max(0, y2));

                return;
            }

            // never attempt to remove this part,
            // if we didn't calculate our new values as floats, we would end up with inaccurate output
            float x1_out = x1, y1_out = y1;
            float x2_out = x2, y2_out = y2;

            // calculate the line slope, and the entercepted part of the y axis
            float m = (y2_out - y1_out) / (x2_out - x1_out);
            float c = y1_out - m * x1_out;

            // handle x1
            if (x1_out < 0)
            {
                x1_out = 0;
                y1_out = c;
            }
            else if (x1_out >= width)
            {
                x1_out = width - 1;
                y1_out = (width - 1) * m + c;
            }

            // handle x2
            if (x2_out < 0)
            {
                x2_out = 0;
                y2_out = c;
            }
            else if (x2_out >= width)
            {
                x2_out = width - 1;
                y2_out = (width - 1) * m + c;
            }

            // handle y1
            if (y1_out < 0)
            {
                x1_out = -c / m;
                y1_out = 0;
            }
            else if (y1_out >= height)
            {
                x1_out = (height - 1 - c) / m;
                y1_out = height - 1;
            }

            // handle y2
            if (y2_out < 0)
            {
                x2_out = -c / m;
                y2_out = 0;
            }
            else if (y2_out >= height)
            {
                x2_out = (height - 1 - c) / m;
                y2_out = height - 1;
            }

            // final check, to avoid lines that are totally outside bounds
            if (x1_out < 0 || x1_out >= width || y1_out < 0 || y1_out >= height)
            {
                x1_out = 0; x2_out = 0;
                y1_out = 0; y2_out = 0;
            }

            if (x2_out < 0 || x2_out >= width || y2_out < 0 || y2_out >= height)
            {
                x1_out = 0; x2_out = 0;
                y1_out = 0; y2_out = 0;
            }

            // replace inputs with new values
            x1 = (int)x1_out; y1 = (int)y1_out;
            x2 = (int)x2_out; y2 = (int)y2_out;
        }

        #endregion
    }
}
