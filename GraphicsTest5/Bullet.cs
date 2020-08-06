using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;

namespace GraphicsTest5
{ 

    class Bullet : GameObject
    {

        public Rectangle rect = new Rectangle();

        private double x_vel = 0;
        private double y_vel = 0;

        public Bullet(double height, double width, double x_pos, double y_pos, Canvas MyCanvas, double vel)
        {
            GetLeft = x_pos;
            GetTop = y_pos;
            Height = height;
            Width = width;
            y_vel = vel;
            rect.Width = Width;
            rect.Height = Height;
            rect.Fill = Brushes.Yellow;
            rect.Stroke = Brushes.White;

            MyCanvas.Children.Add(rect);

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
            getTop += Y_vel;
        }
    }
}

