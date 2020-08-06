using System;
using System.Windows.Shapes;
using System.Windows.Media;
using System.Windows.Controls;
using System.Windows.Media.Imaging;

namespace GraphicsTest5
{
    class EnemyObject : GameObject
    {

        public Rectangle rect = new Rectangle();

        private double x_vel = 3;
        private double y_vel = 0;

        public EnemyObject(double height, double width, double x_pos, double y_pos, Canvas MyCanvas, int inv_num)
        {
            ImageBrush EnemySkin = new ImageBrush();
            GetLeft = x_pos;
            GetTop = y_pos;
            Height = height;
            Width = width;
            rect.Width = Width;
            rect.Height = Height;
            EnemySkin.ImageSource = new BitmapImage(new Uri($"pack://application:,,,/images/invader{inv_num}.gif"));
            //rect.Stroke = Brushes.White;
            rect.Fill = EnemySkin;
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
            getLeft += X_vel;
        }
    }
}
