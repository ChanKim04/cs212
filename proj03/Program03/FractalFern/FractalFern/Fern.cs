using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Windows;
using System.Windows.Controls;
using System.Windows.Data;
using System.Windows.Documents;
using System.Windows.Input;
using System.Windows.Media;
using System.Windows.Media.Imaging;
using System.Windows.Media.Animation;
using System.Windows.Navigation;
using System.Windows.Shapes;

namespace FernNamespace
{
    /*
     * this class draws a fractal fern when the constructor is called.
     * 
     */
    class Fern
    {
        private static int BERRYMIN = 1;
        private static int TENDRILMIN = 1;
        private static int LR = 1;
        private static double DELTATHETA = 0.01;
        private static double SEGLENGTH = 7.0;
        private static Random random = new Random();


        /* 
         * Fern constructor erases screen and draws a fern
         * 
         * Size: number of 1-pixel segments of tendrils
         * Redux: how much smaller children clusters are compared to parents
         * Turnbias: how likely to turn right vs. left (0=always left, 0.5 = 50/50, 1.0 = always right)
         * canvas: the canvas that the fern will be drawn on
         */
        public Fern(double size, double redux, double turnbias, Canvas canvas)
        {
            // randomly select and set the background with image files.
            int randomBackground = random.Next(0, 2);
            if (randomBackground == 0)
            {
                ImageBrush myImageBrush = new ImageBrush();
                myImageBrush.ImageSource = new BitmapImage(new Uri(".../.../image1.jpg", UriKind.Relative));
                canvas.Background = myImageBrush;
            }
            else 
            {
                ImageBrush myImageBrush = new ImageBrush();
                myImageBrush.ImageSource = new BitmapImage(new Uri(".../.../image2.png", UriKind.Relative));
                canvas.Background = myImageBrush;
            }
            canvas.Children.Clear();                                // delete old canvas contents
            // draw a new fern with given parameters
            tendril((int)(canvas.Width / 2), (int)(canvas.Height), size/1.5, redux, turnbias, Math.PI, canvas);
        }

        /*
         * cluster draws a cluster at the given location and then draws a bunch of tendrils out in 
         * regularly-spaced directions out of the cluster.
         */
        private void cluster(int x, int y, double size, double redux, double turnbias, double direction, int LR,Canvas canvas)
        {
            // compute the angle of the outgoing tendril
            double theta = (Math.PI * 10 * random.NextDouble()/180) - Math.PI * 80 / 180;
            tendril(x, y, size, redux, turnbias, direction, canvas);
            // draw left or right tendril depending on the parameter "LR" value, 
            if (LR == 1)
            {
                tendril(x, y, size/1.5, redux, turnbias, direction - theta, canvas);
            }
            else
            {
                tendril(x, y, size/1.5, redux, turnbias, direction + theta, canvas);
            }
        }

        /*
         * tendril draws a tendril (a randomly-wavy line) in the given direction, for the given length, 
         * and draws a cluster at the other end if the line is big enough.
         */
        private void tendril(int x1, int y1, double size, double redux, double turnbias, double direction, Canvas canvas)
        {
            int x2 = x1, y2 = y1;
            // make one direction so that the stem does not too much wavy but make it more smooth
            double directions = (random.NextDouble() < turnbias) ? -1.0 : 1.0;
            for (int i = 0; i < size; i++)
            {
                direction += directions * DELTATHETA;
                x1 = x2; y1 = y2;
                x2 = x1 + (int)(SEGLENGTH * Math.Sin(direction));
                y2 = y1 + (int)(SEGLENGTH * Math.Cos(direction));
                byte red = (byte)(100 + size * 5);
                byte green = (byte)(240 - size * 6);
                //if (size>120) red = 138; green = 108;
                line(x1, y1, x2, y2, red, green, 0, 2 + size / 80, canvas);
            }
            LR = LR * -1;
            if (size > TENDRILMIN)
                cluster(x2, y2, size / redux, redux, turnbias, direction, LR, canvas);
            if (size < BERRYMIN)
                berry(x2, y2, 3, canvas);
        }

        /*
         * draw a red circle centered at (x,y), radius radius, with a black edge, onto canvas
         */
        private void berry(int x, int y, double radius, Canvas canvas)
        {
            // randomly select the color
            int randomColor1 = random.Next(0,255);
            int randomColor2 = random.Next(0, 255);
            Ellipse myEllipse = new Ellipse();
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Color.FromArgb(255, 255, (byte)randomColor1, (byte)randomColor2);
            myEllipse.Fill = mySolidColorBrush;
            myEllipse.StrokeThickness = 1;
            myEllipse.Stroke = Brushes.Olive;
            myEllipse.HorizontalAlignment = HorizontalAlignment.Center;
            myEllipse.VerticalAlignment = VerticalAlignment.Center;
            myEllipse.Width = 2 * radius;
            myEllipse.Height = 2 * radius;
            myEllipse.SetCenter(x, y);
            canvas.Children.Add(myEllipse);
        }

        /*
         * draw a line segment (x1,y1) to (x2,y2) with given color, thickness on canvas
         */
        private void line(int x1, int y1, int x2, int y2, byte r, byte g, byte b, double thickness, Canvas canvas)
        {
            Line myLine = new Line();
            SolidColorBrush mySolidColorBrush = new SolidColorBrush();
            mySolidColorBrush.Color = Color.FromArgb(255, r, g, b);
            myLine.X1 = x1;
            myLine.Y1 = y1;
            myLine.X2 = x2;
            myLine.Y2 = y2;
            myLine.Stroke = mySolidColorBrush;
            myLine.VerticalAlignment = VerticalAlignment.Center;
            myLine.HorizontalAlignment = HorizontalAlignment.Left;
            myLine.StrokeThickness = thickness;
            canvas.Children.Add(myLine);
        }
    }
}

/*
 * this class is needed to enable us to set the center for an ellipse (not built in?!)
 */
public static class EllipseX
{
    public static void SetCenter(this Ellipse ellipse, double X, double Y)
    {
        Canvas.SetTop(ellipse, Y - ellipse.Height / 2);
        Canvas.SetLeft(ellipse, X - ellipse.Width / 2);
    }
}

