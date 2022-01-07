using ProgramTervezesiMintak.Abstractions;
using System;
using System.Collections.Generic;
using System.Drawing;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using System.Windows.Forms;

namespace ProgramTervezesiMintak.Entities
{
    public class Ball: Toy
    {
        public SolidBrush BallBrush { get; private set; }
        private Random random = new Random();

        public Ball(Color kivantszin)
        {
            BallBrush = new SolidBrush(kivantszin);
            Click += Ball_Click;
        }

        private void Ball_Click(object sender, EventArgs e)
        {
            BallBrush.Color = Color.FromArgb(random.Next(256), random.Next(256), random.Next(256));
            Invalidate();
        }

        protected override void DrawImage(Graphics g)
        {
            g.FillEllipse(BallBrush, 0, 0, Width, Height);
        }

        public override void MoveToy()
        {
            base.MoveToy();
            Top += Convert.ToInt32(Math.Sin(1.0 * Left / 10.0));
        }

        protected override string GetTypeName()
        {
            return "labda";
        }
    }
}
