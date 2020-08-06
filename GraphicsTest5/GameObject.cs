

namespace GraphicsTest5
{


    abstract class GameObject
    {
        protected double height;
        protected double width;

        protected bool visible = true;

        protected double getTop;
        protected double getLeft;

        public virtual bool Visible
        {
            get { return visible; }
            set { visible = value; }
        }

        public virtual double Height
        {
            get { return height; }
            set { height = value; }
        }
        public virtual double Width
        {
            get { return width; }
            set { width = value; }
        }
        public virtual double GetTop
        {
            get { return getTop; }
            set { getTop = value; }
        }
        public virtual double GetLeft
        {
            get { return getLeft; }
            set { getLeft = value; }
        }

    }
}

