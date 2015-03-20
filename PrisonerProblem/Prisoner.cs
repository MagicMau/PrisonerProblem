using System.Drawing;

namespace PrisonerProblem
{
    internal class Prisoner
    {
        static Font font;
        static Brush brush;

        static Prisoner()
        {
            font = new Font("Arial", 16f);
            brush = new SolidBrush(Color.White);
        }


        public bool IsStop { get; set; }
        public int Number { get; set; }
        Light light;

        int count = 0, prisonerCount = 0;
        bool isLastPrisoner = false, hasLitLight = false;

        internal Prisoner(int number, Light light, int prisonerCount)
        {
            this.Number = number;
            this.light = light;
            this.prisonerCount = prisonerCount;
            this.isLastPrisoner = number == (prisonerCount - 1);
            IsStop = false;
        }

        internal void Draw(Graphics g)
        {
            g.DrawString(string.Format("Prisoner {0} is being interrogated!", Number), font, brush, new PointF(10, 10));
            CalculateState();
            if (IsStop)
            {
                g.DrawString(string.Format("Prisoner {0} called STOP!", Number), font, brush, new PointF(10, 130));
            }
        }

        private void CalculateState()
        {
            if (isLastPrisoner)
            {
                if (light.IsLit)
                {
                    light.IsLit = false;
                    count++;
                }
                if (count == (prisonerCount - 1))
                {
                    IsStop = true;
                }
            }
            else
            {
                if (!light.IsLit && !hasLitLight)
                {
                    hasLitLight = true;
                    light.IsLit = true;
                }
            }
        }

    }
}