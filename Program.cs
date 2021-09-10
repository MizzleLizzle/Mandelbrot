using System.Drawing;
using System.Drawing.Imaging;
using System.Numerics;

namespace Mandelbrot
{
    class Program
    {
        static void Main(string[] args)
        {
            Complex Topleft = new Complex(-2, 2);
            Complex BottomRight = new Complex(2, -2);
            MandelbrotGenerator Mgen = new MandelbrotGenerator(Topleft, BottomRight, 8000, 8000, 25, 10);
            Bitmap Bmap = Mgen.DrawMandelBrot();
            Bmap.Save("test.png", ImageFormat.Png);
        }
    }
}
