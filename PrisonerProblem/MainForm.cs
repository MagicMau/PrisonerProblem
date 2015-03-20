using System;
using System.Collections.Generic;
using System.ComponentModel;
using System.Data;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace PrisonerProblem
{
    public partial class MainForm : Form
    {
        Bitmap image;
        List<Prisoner> prisoners;
        Light light;
        Random rnd;

        public MainForm()
        {
            InitializeComponent();

            image = new Bitmap(640, 480);
            Reset(5);

            pictureBox1.Image = image;
        }

        private void Reset(int prisonerCount)
        {
            rnd = new Random();
            prisoners = new List<Prisoner>();

            // Create the light
            light = new Light();

            // Create prisoners
            for (int i = 0; i < prisonerCount; i++)
            {
                prisoners.Add(new Prisoner(i, light, prisonerCount));
            }

            // Draw the starting image
            using (Graphics g = Graphics.FromImage(image))
            {
                g.Clear(Color.Black);
                light.Draw(g);
            }
            pictureBox1.Invalidate();

            listBox1.Items.Clear();

            // Start the game
            timer1.Start();

        }

        private void timer1_Tick(object sender, EventArgs e)
        {
            Prisoner randomPrisoner = prisoners[rnd.Next(prisoners.Count)];
            listBox1.Items.Add(randomPrisoner.Number);
            using (Graphics g = Graphics.FromImage(image))
            {
                g.Clear(Color.Black);
                randomPrisoner.Draw(g);
                light.Draw(g);

                if (randomPrisoner.IsStop) {
                    timer1.Stop();
                    Font font = new Font("Arial", 20f, FontStyle.Bold);

                    if (CheckIfOK())
                    {
                        g.DrawString("YOU ARE ALL FREE!", font, new SolidBrush(Color.Green), new Point(10, 150));
                    }
                    else
                    {
                        g.DrawString("YOU ARE ALL DEAD!", font, new SolidBrush(Color.Red), new Point(10, 150));
                    }

                    g.DrawString(string.Format("It only took {0} interrogations. I'm tired.", listBox1.Items.Count),
                        font, new SolidBrush(Color.SlateGray), new Point(10, 180));
                }
            }
            pictureBox1.Invalidate();
        }

        private bool CheckIfOK()
        {
            bool ok = true;
            for (int i = 0; i < prisoners.Count; i++)
            {
                ok = ok && listBox1.Items.Contains(i);
            }
            return ok;
        }

        private void button1_Click(object sender, EventArgs e)
        {
            int pc = 0;
            if (int.TryParse(textBox1.Text, out pc))
            {
                Reset(pc);
            }
        }
    }
}
