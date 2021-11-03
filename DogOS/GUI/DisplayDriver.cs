using System;
using System.Collections.Generic;
using System.Text;
using Cosmos.System.Graphics;
using System.Drawing;

namespace DogOS.GUI
{
    public enum PerferModeType
    {
        Width,
        Height
        // Color depth is not added, since there is no support for other color depths except for 32 bit.
    }

    public class DisplayDriver
    {
        public Canvas screen;
        public int width, height = 0;

        #region Init

        public DisplayDriver()
        {
            // Init with the default values.
            Init();
        }

        public DisplayDriver(int width, int height)
        {
            // Init with custom width and height provided by user / os
            InitWH(width, height);
        }

        private void Init()
        {
            screen = FullScreenCanvas.GetFullScreenCanvas();

            // For some reason, columns and rows are switched.
            // So store them as such.
            width = Convert.ToInt32(screen.Mode.Columns);
            height = Convert.ToInt32(screen.Mode.Rows);
        }

        private void InitWH(int width, int height)
        {
            // Store the width and height.
            this.width = width;
            this.height = height;

            Mode mode;

            // Check if the mode is even supported. ColorDepth32 is selected since other
            if(!ModeIsAvailable(width, height))
            {
                mode = ClosestModeAvailable(width, height);
            }
            else
            {
                mode = new Mode(width, height, ColorDepth.ColorDepth32);
            }

            // Create the screen with the new mode.
            screen = FullScreenCanvas.GetFullScreenCanvas(mode);
            screen.Clear(Color.Blue);
        }

        #endregion;

        #region Mode Tests

        public bool ModeIsAvailable(int width, int height)
        {
            bool res = false;

            // A new mode to test the available modes.
            Mode test_mode = new Mode(width, height, ColorDepth.ColorDepth32);

            // Check if the list contains the mode.
            if(screen.AvailableModes.Contains(test_mode))
            {
                // Change the result
                res = true;
            }

            return res;
        }

        // TODO: Better way to do this?
        public Mode ClosestModeAvailable(int width, int height, PerferModeType perfer = PerferModeType.Width)
        {
            Mode mode;

            switch(perfer)
            {
                // Closest width is preferred
                case PerferModeType.Width:
                    {
                        // Make a list of all the available width's.
                        // A list works fine since we are adding them by the valid mode's index
                        // So multiple values won't be a problem
                        List<int> valid_widths = new List<int>();

                        // TODO: More efficient way to do this like python's one liner
                        foreach (Mode item in screen.AvailableModes)
                        {
                            valid_widths.Add(item.Columns);
                        }

                        // From my knowledge, LINQ is not available unless a plugin is written for it.
                        // So we cannot just order the list, or use List.Min.
                        // We store each result into a dictionary, then sort by lowest number.
                        // The dictionary will be like <width, index>.

                        var widths_dict = new Dictionary<int, int>();

                        // TODO: Maybe a more efficient way to do this as well?
                        for (int i = 0; i < valid_widths.Count; i++)
                        {
                            widths_dict[Math.Abs(width - valid_widths[i])] = i;
                        }

                        // We then sort the keys.
                        int[] keys = new int[widths_dict.Keys.Count];
                        widths_dict.Keys.CopyTo(keys, 0);
                    
                        Array.Sort(keys);
                        mode = screen.AvailableModes[widths_dict[keys[0]]];
                    }
                    break;

                // Closest height is preferred
                case PerferModeType.Height:
                    {
                        List<int> valid_heights = new List<int>();

                        // TODO: More efficient way to do this like python's one liner
                        foreach (Mode item in screen.AvailableModes)
                        {
                            valid_heights.Add(item.Rows);
                        }

                        var heights_dict = new Dictionary<int, int>();

                        // TODO: Maybe a more efficient way to do this as well?
                        for (int i = 0; i < valid_heights.Count; i++)
                        {
                            heights_dict[Math.Abs(height - valid_heights[i])] = i;
                        }

                        int[] keys = new int[heights_dict.Keys.Count];
                        heights_dict.Keys.CopyTo(keys, 0);
                    
                        Array.Sort(keys);
                        mode = screen.AvailableModes[heights_dict[keys[0]]];
                    }
                    break;

                // Not needed, but the compiler is complaining about a unused variable
                default:
                    mode = new Mode();
                    break;
            }

            return mode;
        }

        #endregion;

        #region Drawing Functions

        public void Pixel(int x, int y, Color c, int pen_width = 1)
        {
            var pen = new Pen(c, pen_width);
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

        public void Line(int x_start, int y_start, int x_end, int y_end, Color c, int pen_width = 1)
        {
            var pen = new Pen(c, pen_width);
            screen.DrawLine(pen, x_start, y_start, x_end, y_end);
        }

        public void Rectangle(int x_start, int y_start, int width, int height, Color c, bool filled = false, int pen_width = 1)
        {
            var pen = new Pen(c, pen_width);
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
