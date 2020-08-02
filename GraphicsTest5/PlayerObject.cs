using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Controls;
using System.Windows.Shapes;
using System.Windows.Input;
using System.Windows.Threading;
using System.Windows.Media;
using System.Runtime.CompilerServices;

namespace GraphicsTest5
{



    class PlayerObject : GameObject
    {

        public Rectangle rect = new Rectangle();

        private double x_vel = 0;
        private double y_vel = 0;


        public PlayerObject(double height, double width, double x_pos, double y_pos, Canvas myCanvas)
        {
            GetLeft = x_pos;
            GetTop = y_pos;
            Height = height;
            Width = width;
            rect.Width = Width;
            rect.Height = Height;
            rect.Fill = Brushes.White;
            
            myCanvas.Children.Add(rect);

            Canvas.SetLeft(rect, GetLeft);
            Canvas.SetTop(rect, GetTop);
        }
 
        public override double GetLeft
        {
            get { return getLeft; }
            set { getLeft = value; }
        }
        public override double GetTop
        {
            get { return getTop; }
            set { getTop = value; }
        }
        public double X_vel
        {
            get { return x_vel; }
            set { x_vel = value; }
        }
        public double Y_vel
        {
            get { return y_vel; }
            set { y_vel = value; }
        }
        
        public void Move()
        {
            if ((getLeft <= 0 && x_vel < 0) || (getLeft >= 730 && x_vel > 0 ))
            {
                x_vel = 0;
            }

            getLeft += X_vel;
        }
    }
}