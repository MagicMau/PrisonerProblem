using System;
using System.Drawing;

namespace PrisonerProblem
{
    internal class Light
    {
        public bool IsLit { get; set; }

        Brush brush;
        Pen pen;
        Rectangle rect;

        public Light()
        {
            IsLit = false;
            brush = new SolidBrush(Color.Yellow);
            pen = new Pen(brush);
            rect = new Rectangle(50, 50, 50, 50);
        }

        internal void Draw(Graphics g)
        {
            if (IsLit)
            {
                g.FillEllipse(brush, rect);
            }
            else
            {
                g.DrawEllipse(pen, rect);
            }
        }
    }
}