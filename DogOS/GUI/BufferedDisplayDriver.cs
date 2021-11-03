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

        public BufferedDisplayDriver() : base() {}

        public BufferedDisplayDriver(int width, int height) : base(width, height) {}
    }
}
