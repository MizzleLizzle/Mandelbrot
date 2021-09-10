using System;
using System.Drawing;
using System.Numerics;

namespace Mandelbrot
{
    class MandelbrotGenerator
    {
        private int Iterations;
        private double Threshold;
        private Bitmap Bmap;
        private Complex TopleftCorner;
        private Complex BottomrightCorner;
        public MandelbrotGenerator(Complex TopleftCorner, Complex BottomrightCorner, int width, int height, int Iterations, double Threshold)
        {
            this.Bmap = new Bitmap(width, height);
            this.TopleftCorner = TopleftCorner;
            this.BottomrightCorner = BottomrightCorner;
            this.Threshold = Threshold;
            this.Iterations = Iterations;
        }

        private Complex f(Complex x, Complex c)
        {
            return x * x + c;
        }

        public Bitmap DrawMandelBrot()
        {
            double WidthStep = Math.Abs(BottomrightCorner.Real - TopleftCorner.Real) / Bmap.Width;
            double HeightStep = Math.Abs(TopleftCorner.Imaginary - BottomrightCorner.Imaginary) / Bmap.Height;
            for(int x  = 0; x < Bmap.Width; x++)
            {
                for(int y = 0; y < Bmap.Height; y++)
                {
                    Bmap.SetPixel(x, y, PointToColor(new Complex(TopleftCorner.Real + x * WidthStep, BottomrightCorner.Imaginary + y * HeightStep)));
                }
            }
            return Bmap;
        }

        private Color PointToColor(Complex c)
        {
            Complex current = new Complex(0,0);
            int iter = 0;
            while(iter < Iterations && current.Magnitude < Threshold)
            {
                current = f(current, c);
                iter++;
            }
            if(iter < Iterations)
            {
                return ValueToColor((double)iter / Iterations);
            }
            return Color.FromArgb(0, 0, 0);
        }
        public static Color ValueToColor(double Value)
        {
            if (Value < 1.0/6.0)
            {
                int Green = Convert.ToInt32(255.0 * (Value / (1.0/6.0)));

                return Color.FromArgb(255, Green, 0);
            }
            if (Value < 2.0/6.0)
            {
                int Red = Convert.ToInt32(255.0 - (255.0 * (Value - 1.0/6.0) / (1.0 / 6.0)));

                return Color.FromArgb(Red, 255, 0);
            }
            if (Value < 1.0/2.0)
            {
                int Blue = Convert.ToInt32(255.0 * (Value - 2.0/6.0) / (1.0/6.0));

                return Color.FromArgb(0, 255, Blue);
            }
            if (Value < 4.0/6.0)
            {
                int Green = Convert.ToInt32(255.0 - 255.0 * (Value - 1.0/2.0) / (1.0/6.0));

                return Color.FromArgb(0, Green, 255);
            }
            if (Value < 5.0/6.0)
            {
                int Red = Convert.ToInt32(255.0 * (Value - 4.0/6.0) / (1.0/6.0));

                return Color.FromArgb(Red, 0, 255);
            }
            else
            {
                int Blue = Convert.ToInt32(255.0 - 255.0 * (Value - 5.0/6.0) / (1.0/6.0));

                return Color.FromArgb(255, 0, Blue);
            }
        }
    }
}
