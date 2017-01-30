using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace Thylacine.Exceptions
{
    public class ImageSizeException : Exception
    {
        public int Width { get; }
        public int Height { get; }

        public int ExpectedWidth { get; }
        public int ExpectedHeight { get; }

        internal ImageSizeException(int width, int height, int expectedWidth, int expectedHeight) : base("The image was to large. Was expecting " + expectedWidth + "x" +expectedHeight+" but got " + width + "x" + height + "height")
        {
            this.Width = width;
            this.Height = height;
            this.ExpectedWidth = expectedWidth;
            this.ExpectedHeight = expectedHeight;
        }
    }
}
